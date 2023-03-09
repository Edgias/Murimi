using NigTech.Murimi.ApplicationCore.Entities;
using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;
using NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate;
using NigTech.Murimi.ApplicationCore.Interfaces;
using NigTech.Murimi.ApplicationCore.SharedKernel;
using NigTech.Murimi.ApplicationCore.Specifications;

namespace NigTech.Murimi.ApplicationCore.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly IAsyncRepository<NumberSequence> _numberSequenceRepository;
        private readonly IAsyncRepository<PriceList> _priceListRepository;
        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IAsyncRepository<SalesOrder> _salesOrderRepository;
        private readonly IAsyncRepository<Contact> _contactRepository;
        private readonly IAsyncRepository<Corporate> _corporateRepository;
        private readonly IAsyncRepository<Quotation> _quotationRepository;

        public QuotationService(IAsyncRepository<NumberSequence> numberSequenceRepository,
            IAsyncRepository<PriceList> priceListRepository,
            IAsyncRepository<Item> itemRepository,
            IAsyncRepository<SalesOrder> salesOrderRepository,
            IAsyncRepository<Contact> contactRepository,
            IAsyncRepository<Corporate> corporateRepository,
            IAsyncRepository<Quotation> quotationRepository)
        {
            _numberSequenceRepository = numberSequenceRepository;
            _priceListRepository = priceListRepository;
            _itemRepository = itemRepository;
            _salesOrderRepository = salesOrderRepository;
            _contactRepository = contactRepository;
            _corporateRepository = corporateRepository;
            _quotationRepository = quotationRepository;
        }

        public async Task AddQuotationItemAsync(Guid quotationId, Guid itemId, Guid priceListId, Guid? taxId, int units = 1)
        {
            Guard.AgainstNull(quotationId, nameof(quotationId));
            Guard.AgainstNull(priceListId, nameof(priceListId));
            Guard.AgainstNull(itemId, nameof(itemId));

            Quotation? quotation = await _quotationRepository.GetByIdAsync(quotationId);

            Item? item = await _itemRepository.GetByIdAsync(itemId);
            ItemQuoted itemQuoted = new(item!.Id, item.Name);

            PriceList? priceList = await _priceListRepository.GetByIdAsync(priceListId);

            QuotationItem quotationItem = new(itemQuoted, priceList!.UnitPrice, units, taxId);

            quotation!.AddItem(quotationItem);

            await _quotationRepository.UpdateAsync(quotation);
        }

        public async Task ConvertToSalesOrderAsync(Guid quotationId)
        {
            Quotation? quotation = 
                await _quotationRepository.GetSingleBySpecificationAsync(new QuotationSpecification(quotationId));

            Address shipToAddress = null!;
            if (quotation!.ContactId.HasValue)
            {
                Contact? contact =
                    await _contactRepository.GetByIdAsync(quotation.ContactId.Value);
                shipToAddress = contact!.ContactAddress!;
            }

            if (quotation.CorporateId.HasValue)
            {
                Corporate? corporate =
                    await _corporateRepository.GetByIdAsync(quotation.CorporateId.Value);
                shipToAddress = corporate!.CorporateAddress!;
            }

            List<SalesOrderItem> salesOrderItems = quotation.QuotationItems.Select(quotationItem =>
            {
                ItemOrdered itemOrdered = new(quotationItem.ItemQuoted.ItemId, 
                    quotationItem.ItemQuoted.ItemName);

                SalesOrderItem salesOrderItem = new(itemOrdered, quotationItem.UnitPrice, quotationItem.Units);

                return salesOrderItem;
            }).ToList();

            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(typeof(SalesOrder).Name));

            int salesOrderCount = await _salesOrderRepository.CountAllAsync();
            string salesOrderNumber = numberSequence!.GenerateSequence(salesOrderCount);

            SalesOrder salesOrder = new(salesOrderNumber, quotation.ContactId, quotation.CorporateId, 
                quotationId, shipToAddress, salesOrderItems);

            await _salesOrderRepository.AddAsync(salesOrder);
        }

        public async Task<Quotation> CreateQuotationAsync(DateTimeOffset quotationDate, DateTimeOffset expiryDate, 
            Guid? contactId, Guid? corporateId)
        {
            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(typeof(Quotation).Name));

            int quotationCount = await _quotationRepository.CountAllAsync();
            string quotationNumber = numberSequence!.GenerateSequence(quotationCount);

            Quotation quotation = new(quotationNumber, quotationDate, expiryDate, contactId, corporateId);

            quotation = await _quotationRepository.AddAsync(quotation);

            return quotation;
        }

        public async Task<Quotation> CreateQuotationAsync(DateTimeOffset quotationDate, 
            DateTimeOffset expiryDate, List<QuotationItem> quotationItems, 
            Guid? contactId, Guid? corporateId)
        {
            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(typeof(Quotation).Name));

            int quotationCount = await _quotationRepository.CountAllAsync();
            string quotationNumber = numberSequence!.GenerateSequence(quotationCount);

            Quotation quotation = new(quotationNumber, quotationDate, expiryDate, quotationItems, contactId, corporateId);

            quotation = await _quotationRepository.AddAsync(quotation);

            return quotation;
        }
    }
}
