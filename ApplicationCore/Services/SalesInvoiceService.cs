using NigTech.Murimi.ApplicationCore.Entities;
using NigTech.Murimi.ApplicationCore.Entities.SalesInvoiceAggregate;
using NigTech.Murimi.ApplicationCore.Interfaces;
using NigTech.Murimi.ApplicationCore.SharedKernel;
using NigTech.Murimi.ApplicationCore.Specifications;

namespace NigTech.Murimi.ApplicationCore.Services
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly IAsyncRepository<NumberSequence> _numberSequenceRepository;
        private readonly IAsyncRepository<PriceList> _priceListRepository;
        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IAsyncRepository<SalesInvoice> _salesInvoiceRepository;
        private readonly IAsyncRepository<Contact> _contactRepository;
        private readonly IAsyncRepository<Corporate> _corporateRepository;
        private readonly IAsyncRepository<SalesInvoiceNote> _salesInvoiceNoteRepository;

        public SalesInvoiceService(IAsyncRepository<NumberSequence> numberSequenceRepository,
            IAsyncRepository<PriceList> priceListRepository,
            IAsyncRepository<Item> itemRepository,
            IAsyncRepository<SalesInvoice> salesInvoiceRepository,
            IAsyncRepository<Contact> contactRepository,
            IAsyncRepository<Corporate> corporateRepository,
            IAsyncRepository<SalesInvoiceNote> salesInvoiceNoteRepository)
        {
            _numberSequenceRepository = numberSequenceRepository;
            _priceListRepository = priceListRepository;
            _itemRepository = itemRepository;
            _salesInvoiceRepository = salesInvoiceRepository;
            _contactRepository = contactRepository;
            _corporateRepository = corporateRepository;
            _salesInvoiceNoteRepository = salesInvoiceNoteRepository;
        }

        public async Task AddInvoiceItemAsync(Guid invoiceId, Guid itemId, Guid priceListId, Guid? taxId, int units = 1)
        {
            SalesInvoice? salesInvoice = await _salesInvoiceRepository.GetByIdAsync(invoiceId);

            Item? item = await _itemRepository.GetByIdAsync(itemId);
            InvoicedItem invoicedItem = new(item!.Id, item.Name);

            PriceList? priceList = await _priceListRepository.GetByIdAsync(priceListId);
            SalesInvoiceItem salesInvoiceItem = new(invoicedItem, priceList!.UnitPrice, units, taxId);

            salesInvoice!.AddItem(salesInvoiceItem);

            await _salesInvoiceRepository.UpdateAsync(salesInvoice);
        }

        public async Task<SalesInvoice> CreateInvoiceAsync(DateTimeOffset dueDate, Guid? invoiceNoteId, 
            Guid? contactId, Guid? corporateId, Guid? salesOrderId)
        {
            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(typeof(SalesInvoice).Name));

            int invoiceCount = await _salesInvoiceRepository.CountAllAsync();

            string invoiceNumber = numberSequence!.GenerateSequence(invoiceCount);

            Address billingAddress = null!;
            if (contactId.HasValue)
            {
                Contact? contact =
                    await _contactRepository.GetByIdAsync(contactId.Value);
                billingAddress = contact!.ContactAddress!;
            }

            if (corporateId.HasValue)
            {
                Corporate? corporate =
                    await _corporateRepository.GetByIdAsync(corporateId.Value);
                billingAddress = corporate!.CorporateAddress!;
            }

            string? invoiceNotes = string.Empty;

            if(invoiceNoteId.HasValue)
            {
                SalesInvoiceNote? salesInvoiceNote = await _salesInvoiceNoteRepository.GetByIdAsync(invoiceNoteId.Value);
                invoiceNotes = salesInvoiceNote?.Description;
            }

            SalesInvoice salesInvoice = new(invoiceNumber, dueDate, invoiceNotes, contactId, corporateId, 
                salesOrderId, billingAddress);

            salesInvoice = await _salesInvoiceRepository.AddAsync(salesInvoice);

            return salesInvoice;
        }

        public async Task<SalesInvoice> CreateInvoiceAsync(DateTimeOffset dueDate, 
            Guid? invoiceNoteId, List<SalesInvoiceItem> invoiceItems, 
            Guid? contactId, Guid? corporateId, Guid? salesOrderId)
        {
            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(typeof(SalesInvoice).Name));

            int invoiceCount = await _salesInvoiceRepository.CountAllAsync();

            string invoiceNumber = numberSequence!.GenerateSequence(invoiceCount);

            Address billingAddress = null!;
            if (contactId.HasValue)
            {
                Contact? contact =
                    await _contactRepository.GetByIdAsync(contactId.Value);

                billingAddress = contact!.ContactAddress!;
            }

            if (corporateId.HasValue)
            {
                Corporate? corporate =
                    await _corporateRepository.GetByIdAsync(corporateId.Value);

                billingAddress = corporate!.CorporateAddress!;
            }

            string? invoiceNotes = string.Empty;

            if (invoiceNoteId.HasValue)
            {
                SalesInvoiceNote? salesInvoiceNote = await _salesInvoiceNoteRepository.GetByIdAsync(invoiceNoteId.Value);
                invoiceNotes = salesInvoiceNote?.Description;
            }

            SalesInvoice invoice = new(invoiceNumber, dueDate, invoiceNotes, contactId, corporateId, 
                salesOrderId, billingAddress, invoiceItems);

            invoice = await _salesInvoiceRepository.AddAsync(invoice);

            return invoice;
        }
    }
}
