using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;

namespace NigTech.Murimi.ApplicationCore.Interfaces
{
    public interface IQuotationService
    {
        Task AddQuotationItemAsync(Guid quotationId, Guid itemId, Guid priceListId, Guid? taxId, int units = 1);

        Task ConvertToSalesOrderAsync(Guid quotationId);

        Task<Quotation> CreateQuotationAsync(DateTimeOffset quotationDate, DateTimeOffset expiryDate, 
            Guid? contactId, Guid? corporateId);

        Task<Quotation> CreateQuotationAsync(DateTimeOffset quotationDate, DateTimeOffset expiryDate, 
            List<QuotationItem> quotationItems, Guid? contactId, Guid? corporateId);

    }
}
