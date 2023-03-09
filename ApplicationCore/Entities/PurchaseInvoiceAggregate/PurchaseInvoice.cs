using NigTech.Murimi.ApplicationCore.Entities.PurchaseOrderAggregate;
using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.PurchaseInvoiceAggregate
{
    public class PurchaseInvoice : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public DateTimeOffset InvoiceDate { get; private set; } = DateTimeOffset.Now;

        public DateTimeOffset DueDate { get; private set; }

        public Address BillingAddress { get; private set; } = null!;

        public string? InvoiceNotes { get; private set; }

        public Guid PurchaseOrderId { get; private set; }

        public PurchaseOrder PurchaseOrder { get; private set; } = null!;

        public Guid SupplierId { get; private set; }

        public Supplier Supplier { get; private set; } = null!;

        private readonly List<PurchaseInvoiceItem> _purchaseInvoiceItems = new();

        public IReadOnlyCollection<PurchaseInvoiceItem> PurchaseInvoiceItems => _purchaseInvoiceItems.AsReadOnly();

        private PurchaseInvoice()
        {
            // Required by EF
        }

        public PurchaseInvoice(string name, DateTimeOffset dueDate, string? invoiceNotes, 
            Guid supplierId, Guid purchaseOrderId, Address billingAddress)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(dueDate, nameof(dueDate));

            Name = name;
            BillingAddress = billingAddress;
            InvoiceNotes = invoiceNotes;
            DueDate = dueDate;
            PurchaseOrderId = purchaseOrderId;
            SupplierId = supplierId;
        }

        public PurchaseInvoice(string name, DateTimeOffset dueDate, string? invoiceNotes,
            Guid supplierId, Guid purchaseOrderId, Address billingAddress, List<PurchaseInvoiceItem> purchaseInvoiceItems)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(dueDate, nameof(dueDate));
            Guard.AgainstNull(purchaseInvoiceItems, nameof(purchaseInvoiceItems));

            Name = name;
            BillingAddress = billingAddress;
            InvoiceNotes = invoiceNotes;
            DueDate = dueDate;
            PurchaseOrderId = purchaseOrderId;
            SupplierId = supplierId;
            _purchaseInvoiceItems = purchaseInvoiceItems;
        }

        public void AddItem(PurchaseInvoiceItem purchaseInvoiceItem)
        {
            Guard.AgainstNull(purchaseInvoiceItem, nameof(purchaseInvoiceItem));

            _purchaseInvoiceItems.Add(purchaseInvoiceItem);
        }

        public decimal Total()
        {
            decimal total = 0m;

            foreach (PurchaseInvoiceItem purchaseInvoiceItem in _purchaseInvoiceItems)
            {
                decimal lineTotalBeforeTax = purchaseInvoiceItem.UnitPrice * purchaseInvoiceItem.Units;

                decimal lineTotalAfterTax = purchaseInvoiceItem.TaxId != null ? 
                    (lineTotalBeforeTax + (lineTotalBeforeTax * (purchaseInvoiceItem.Tax!.Percentage / 100))) : 
                    lineTotalBeforeTax;

                total += lineTotalAfterTax;
            }

            return total;
        }

        public decimal TotalTax()
        {
            decimal total = 0m;

            foreach (PurchaseInvoiceItem purchaseInvoiceItem in _purchaseInvoiceItems)
            {
                decimal lineTotalBeforeTax = purchaseInvoiceItem.UnitPrice * purchaseInvoiceItem.Units;

                decimal lineTax = purchaseInvoiceItem.TaxId != null ? 
                    (lineTotalBeforeTax * (purchaseInvoiceItem.Tax!.Percentage / 100)) 
                    : 0;
                total += lineTax;
            }

            return total;
        }

        public decimal TotalWithoutTax()
        {
            decimal total = 0m;

            foreach (PurchaseInvoiceItem purchaseInvoiceItem in _purchaseInvoiceItems)
            {
                decimal lineTotal = purchaseInvoiceItem.UnitPrice * purchaseInvoiceItem.Units;
                total += lineTotal;
            }

            return total;
        }

    }
}
