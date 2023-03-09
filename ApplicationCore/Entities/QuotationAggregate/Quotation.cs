using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate
{
    public class Quotation : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public DateTimeOffset QuotationDate { get; private set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset ExpiryDate { get; private set; }

        public Guid? ContactId { get; private set; }

        public Contact? Contact { get; private set; } = null!;

        public Guid? CorporateId { get; private set; }

        public Corporate? Corporate { get; private set; }

        private readonly List<QuotationItem> _quotationItems = new();

        public IReadOnlyCollection<QuotationItem> QuotationItems => _quotationItems.AsReadOnly();

        private Quotation()
        {
            // Required by EF
        }

        public Quotation(string name, DateTimeOffset quotationDate, DateTimeOffset expiryDate, 
            Guid? contactId, Guid? corporateId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(quotationDate, nameof(quotationDate));
            Guard.AgainstNull(expiryDate, nameof(expiryDate));

            Name = name;
            QuotationDate = quotationDate;
            ExpiryDate = expiryDate;
            ContactId = contactId;
            CorporateId = corporateId;
        }

        public Quotation(string name, DateTimeOffset quotationDate, DateTimeOffset expiryDate, 
            List<QuotationItem> quotationItems, Guid? contactId, Guid? corporateId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(quotationDate, nameof(quotationDate));
            Guard.AgainstNull(expiryDate, nameof(expiryDate));
            Guard.AgainstNull(quotationItems, nameof(quotationItems));

            Name = name;
            QuotationDate = quotationDate;
            ExpiryDate = expiryDate;
            ContactId = contactId;
            CorporateId = corporateId;
            _quotationItems = quotationItems;
        }

        public void AddItem(QuotationItem quotationItem)
        {
            Guard.AgainstNull(quotationItem, nameof(quotationItem));

            _quotationItems.Add(quotationItem);
        }

        public decimal Total()
        {
            decimal total = 0m;

            foreach (QuotationItem quotationItem in _quotationItems)
            {
                decimal lineTotalBeforeTax = quotationItem.UnitPrice * quotationItem.Units;
                decimal lineTotalAfterTax = quotationItem.TaxId != null ? 
                    (lineTotalBeforeTax + (lineTotalBeforeTax * (quotationItem.Tax!.Percentage / 100))) : lineTotalBeforeTax;
                total += lineTotalAfterTax;
            }

            return total;
        }

        public decimal TotalTax()
        {
            decimal total = 0m;

            foreach (QuotationItem quotationItem in _quotationItems)
            {
                decimal lineTotalBeforeTax = quotationItem.UnitPrice * quotationItem.Units;
                decimal lineTax = quotationItem.TaxId != null ? 
                    (lineTotalBeforeTax * (quotationItem.Tax!.Percentage / 100)) : 0;
                total += lineTax;
            }

            return total;
        }

        public decimal TotalWithoutTax()
        {
            decimal total = 0m;

            foreach (QuotationItem quotationItem in _quotationItems)
            {
                decimal lineTotal = quotationItem.UnitPrice * quotationItem.Units;
                total += lineTotal;
            }

            return total;
        }

        public void ChangeExpiryDate(DateTimeOffset expiryDate)
        {
            ExpiryDate = expiryDate;
        }

    }
}
