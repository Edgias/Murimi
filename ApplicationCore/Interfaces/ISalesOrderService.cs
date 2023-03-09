using NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate;

namespace NigTech.Murimi.ApplicationCore.Interfaces
{
    public interface ISalesOrderService
    {
        Task AddSalesOrderItemAsync(Guid salesOrderId, Guid itemId, Guid priceListId, int units = 1);

        Task ConvertToInvoiceAsync(Guid salesOrderId);

        Task<SalesOrder> CreateSalesOrderAsync(Guid? contactId, Guid? corporateId, Guid? quotationId);
    }
}
