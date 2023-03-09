using NigTech.Murimi.ApplicationCore.Entities;
using NigTech.Murimi.ApplicationCore.Entities.SalesInvoiceAggregate;
using NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate;
using NigTech.Murimi.ApplicationCore.Interfaces;
using NigTech.Murimi.ApplicationCore.SharedKernel;
using NigTech.Murimi.ApplicationCore.Specifications;

namespace NigTech.Murimi.ApplicationCore.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly IAsyncRepository<NumberSequence> _numberSequenceRepository;
        private readonly IAsyncRepository<PriceList> _priceListRepository;
        private readonly IAsyncRepository<Item> _itemRepository;
        private readonly IAsyncRepository<Contact> _contactRepository;
        private readonly IAsyncRepository<Corporate> _corporateRepository;
        private readonly IAsyncRepository<SalesInvoiceNote> _salesInvoiceNoteRepository;
        private readonly IAsyncRepository<SalesInvoice> _salesInvoiceRepository;
        private readonly IAsyncRepository<SalesOrder> _salesOrderRepository;

        public SalesOrderService(IAsyncRepository<NumberSequence> numberSequenceRepository,
            IAsyncRepository<PriceList> priceListRepository,
            IAsyncRepository<Item> itemRepository,
            IAsyncRepository<Contact> customerRepository,
            IAsyncRepository<Corporate> corporateRepository,
            IAsyncRepository<SalesInvoiceNote> invoiceNoteRepository,
            IAsyncRepository<SalesInvoice> salesInvoiceRepository,
            IAsyncRepository<SalesOrder> salesOrderRepository)
        {
            _numberSequenceRepository = numberSequenceRepository;
            _priceListRepository = priceListRepository;
            _itemRepository = itemRepository;
            _contactRepository = customerRepository;
            _corporateRepository = corporateRepository;
            _salesInvoiceNoteRepository = invoiceNoteRepository;
            _salesInvoiceRepository = salesInvoiceRepository;
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task AddSalesOrderItemAsync(Guid salesOrderId, Guid itemId, Guid priceListId, int units = 1)
        {
            SalesOrder? salesOrder = await _salesOrderRepository.GetByIdAsync(salesOrderId);

            Item? item = await _itemRepository.GetByIdAsync(itemId);
            ItemOrdered itemQuoted = new(item!.Id, item.Name);

            PriceList? priceList = await _priceListRepository.GetByIdAsync(priceListId);
            SalesOrderItem salesOrderItem = new(itemQuoted, priceList!.UnitPrice, units);
            salesOrder!.AddItem(salesOrderItem);

            await _salesOrderRepository.UpdateAsync(salesOrder);
        }

        public async Task ConvertToInvoiceAsync(Guid salesOrderId)
        {
            SalesOrder? salesOrder = 
                await _salesOrderRepository.GetSingleBySpecificationAsync(new SalesOrderSpecification(salesOrderId));

            Address billingAddress = null!;
            if (salesOrder!.ContactId.HasValue)
            {
                Contact? contact =
                    await _contactRepository.GetByIdAsync(salesOrder.ContactId.Value);
                billingAddress = contact!.ContactAddress!;
            }

            if (salesOrder.CorporateId.HasValue)
            {
                Corporate? corporate =
                    await _corporateRepository.GetByIdAsync(salesOrder.CorporateId.Value);
                billingAddress = corporate!.CorporateAddress!;
            }

            List<SalesInvoiceItem> invoiceLines = salesOrder.SalesOrderItems.Select(salesOrderLine =>
            {
                InvoicedItem invoicedItem = new(salesOrderLine.ItemOrdered.ItemId,
                    salesOrderLine.ItemOrdered.ItemName);

                SalesInvoiceItem invoiceLine = new(invoicedItem, salesOrderLine.UnitPrice, salesOrderLine.Units, null);

                return invoiceLine;
            }).ToList();

            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(typeof(SalesInvoice).Name));

            int invoiceCount = await _salesInvoiceRepository.CountAllAsync();
            string invoiceNumber = numberSequence!.GenerateSequence(invoiceCount);

            SalesInvoiceNote? invoiceNote = 
                await _salesInvoiceNoteRepository.GetSingleBySpecificationAsync(new SalesInvoiceNoteSpecification());

            SalesInvoice invoice = new(invoiceNumber, DateTime.Now,
                invoiceNote?.Description, salesOrder.ContactId, salesOrder.CorporateId, salesOrderId, 
                billingAddress, invoiceLines);

            await _salesInvoiceRepository.AddAsync(invoice);
        }

        public async Task<SalesOrder> CreateSalesOrderAsync(Guid? contactId, Guid? corporateId, Guid? quotationId)
        {
            string salesOrderName = typeof(SalesOrder).Name;
            NumberSequence? numberSequence = 
                await _numberSequenceRepository.GetSingleBySpecificationAsync(
                    new NumberSequenceSpecification(salesOrderName));

            int salesOrderCount = await _salesOrderRepository.CountAllAsync();
            string salesOrderNumber = numberSequence!.GenerateSequence(salesOrderCount);

            Address shipToAddress = null!;

            if (contactId.HasValue)
            {
                Contact? contact =
                    await _contactRepository.GetByIdAsync(contactId.Value);
                shipToAddress = contact!.ContactAddress!;
            }

            if (corporateId.HasValue)
            {
                Corporate? corporate =
                    await _corporateRepository.GetByIdAsync(corporateId.Value);
                shipToAddress = corporate!.CorporateAddress!;
            }

            if (shipToAddress == null)
            {
                throw new Exception("Shipping address cannot be null.");
            }

            SalesOrder salesOrder = new(salesOrderNumber, contactId, corporateId, quotationId, shipToAddress);

            salesOrder = await _salesOrderRepository.AddAsync(salesOrder);

            return salesOrder;
        }
    }
}
