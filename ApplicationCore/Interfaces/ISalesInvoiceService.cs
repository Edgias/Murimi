using NigTech.Murimi.ApplicationCore.Entities.SalesInvoiceAggregate;

namespace NigTech.Murimi.ApplicationCore.Interfaces
{
    public interface ISalesInvoiceService
    {
        Task AddInvoiceItemAsync(Guid invoiceId, Guid itemId, Guid priceListId, Guid? taxId, int units = 1);

        Task<SalesInvoice> CreateInvoiceAsync(DateTimeOffset dueDate, Guid? invoiceNoteId, 
            Guid? contactId, Guid? corporateId, Guid? salesOrderId);

        Task<SalesInvoice> CreateInvoiceAsync(DateTimeOffset dueDate, Guid? invoiceNoteId, 
            List<SalesInvoiceItem> invoiceItems, Guid? contactId, Guid? corporateId, Guid? salesOrderId);
    }
}
