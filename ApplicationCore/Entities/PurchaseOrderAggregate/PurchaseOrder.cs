using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;
using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.PurchaseOrderAggregate
{
    public class PurchaseOrder : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;

        public Address ShipToAddress { get; private set; } = null!;

        public Guid SupplierId { get; private set; }

        public Supplier Supplier { get; private set; } = null!;

        public Guid? QuotationId { get; private set; }

        public Quotation? Quotation { get; private set; }

        private readonly List<PurchaseOrderItem> _purchaseOrderItems = new();

        public IReadOnlyCollection<PurchaseOrderItem> PurchaseOrderItems => _purchaseOrderItems.AsReadOnly();

        private PurchaseOrder()
        {
            // Required by EF
        }

        public PurchaseOrder(string name, Guid supplierId, Guid? quotationId, Address shipToAddress)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(shipToAddress, nameof(shipToAddress));

            Name = name;
            SupplierId = supplierId;
            QuotationId = quotationId;
            ShipToAddress = shipToAddress;
        }

        public PurchaseOrder(string name, Guid supplierId, Guid? quotationId, 
            Address shipToAddress, List<PurchaseOrderItem> salesOrderItems)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(shipToAddress, nameof(shipToAddress));
            Guard.AgainstNull(salesOrderItems, nameof(salesOrderItems));

            Name = name;
            SupplierId = supplierId;
            QuotationId = quotationId;
            ShipToAddress = shipToAddress;
            _purchaseOrderItems = salesOrderItems;
        }

        public void AddItem(PurchaseOrderItem salesOrderItem)
        {
            Guard.AgainstNull(salesOrderItem, nameof(salesOrderItem));

            _purchaseOrderItems.Add(salesOrderItem);
        }

        public decimal Total()
        {
            decimal total = 0m;

            foreach (PurchaseOrderItem salesOrderItem in _purchaseOrderItems)
            {
                total += salesOrderItem.UnitPrice * salesOrderItem.Units;
            }

            return total;
        }
    }
}
