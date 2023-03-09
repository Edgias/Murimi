using NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate;
using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.SalesInvoiceAggregate
{
    public class SalesInvoice : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public DateTimeOffset InvoiceDate { get; private set; } = DateTimeOffset.Now;

        public DateTimeOffset DueDate { get; private set; }

        public Address BillingAddress { get; private set; } = null!;

        public string? InvoiceNotes { get; private set; }

        public Guid? SalesOrderId { get; private set; }

        public SalesOrder? SalesOrder { get; private set; }

        public Guid? ContactId { get; private set; }

        public Contact? Contact { get; private set; } = null!;

        public Guid? CorporateId { get; private set; }

        public Corporate? Corporate { get; private set; }

        private readonly List<SalesInvoiceItem> _salesInvoiceItems = new();
        public IReadOnlyCollection<SalesInvoiceItem> SalesInvoiceItems =>
            _salesInvoiceItems.AsReadOnly();

        private SalesInvoice()
        {
            // Required by EF
        }

        public SalesInvoice(string name, DateTimeOffset dueDate, string? invoiceNotes, 
            Guid? contactId, Guid? corporateId, Guid? salesOrderId, Address billingAddress)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(dueDate, nameof(dueDate));
            Guard.AgainstNull(billingAddress, nameof(billingAddress));

            Name = name;
            BillingAddress = billingAddress;
            InvoiceNotes = invoiceNotes;
            DueDate = dueDate;
            SalesOrderId = salesOrderId;
            ContactId = contactId;
            CorporateId = corporateId;
        }

        public SalesInvoice(string name, DateTimeOffset dueDate, string? invoiceNotes,
            Guid? contactId, Guid? corporateId, Guid? salesOrderId, Address billingAddress,
            List<SalesInvoiceItem> salesInvoiceItems)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(dueDate, nameof(dueDate));
            Guard.AgainstNull(billingAddress, nameof(billingAddress));
            Guard.AgainstNull(salesInvoiceItems, nameof(salesInvoiceItems));

            Name = name;
            BillingAddress = billingAddress;
            InvoiceNotes = invoiceNotes;
            DueDate = dueDate;
            SalesOrderId = salesOrderId;
            ContactId = contactId;
            CorporateId = corporateId;
            _salesInvoiceItems = salesInvoiceItems;
        }

        public void AddItem(SalesInvoiceItem salesInvoiceItem)
        {
            Guard.AgainstNull(salesInvoiceItem, nameof(salesInvoiceItem));

            _salesInvoiceItems.Add(salesInvoiceItem);
        }

        public decimal Total()
        {
            decimal total = 0m;

            foreach (SalesInvoiceItem invoiceLine in _salesInvoiceItems)
            {
                decimal lineTotalBeforeTax = invoiceLine.UnitPrice * invoiceLine.Units;

                decimal lineTotalAfterTax = invoiceLine.TaxId.HasValue ? (lineTotalBeforeTax +
                    (lineTotalBeforeTax * (invoiceLine.Tax!.Percentage / 100))) : lineTotalBeforeTax;

                total += lineTotalAfterTax;
            }

            return total;
        }

        public decimal TotalTax()
        {
            decimal total = 0m;

            foreach (SalesInvoiceItem invoiceLine in _salesInvoiceItems)
            {
                decimal lineTotalBeforeTax = invoiceLine.UnitPrice * invoiceLine.Units;

                decimal lineTax = invoiceLine.TaxId.HasValue ? (lineTotalBeforeTax * (invoiceLine.Tax!.Percentage / 100)) : 0;

                total += lineTax;
            }

            return total;
        }

        public decimal TotalWithoutTax()
        {
            decimal total = 0m;

            foreach (SalesInvoiceItem invoiceLine in _salesInvoiceItems)
            {
                decimal lineTotal = invoiceLine.UnitPrice * invoiceLine.Units;

                total += lineTotal;
            }

            return total;
        }

    }
}
