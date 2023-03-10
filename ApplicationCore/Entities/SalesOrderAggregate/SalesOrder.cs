using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;
using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate
{
    public class SalesOrder : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;

        public Address ShipToAddress { get; private set; } = null!;

        public Guid? ContactId { get; private set; }

        public Contact? Contact { get; private set; } = null!;

        public Guid? CorporateId { get; private set; }

        public Corporate? Corporate { get; private set; }

        public Guid? QuotationId { get; private set; }

        public Quotation? Quotation { get; private set; }

        private readonly List<SalesOrderItem> _salesOrderItems = new();

        public IReadOnlyCollection<SalesOrderItem> SalesOrderItems => _salesOrderItems.AsReadOnly();

        private SalesOrder()
        {
            // Required by EF
        }

        public SalesOrder(string name, Guid? contactId, Guid? corporateId, Guid? quotationId, Address shipToAddress)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(shipToAddress, nameof(shipToAddress));

            Name = name;
            ContactId = contactId;
            CorporateId = corporateId;
            QuotationId = quotationId;
            ShipToAddress = shipToAddress;
        }

        public SalesOrder(string name, Guid? contactId, Guid? corporateId, Guid? quotationId, 
            Address shipToAddress, List<SalesOrderItem> salesOrderItems)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(shipToAddress, nameof(shipToAddress));
            Guard.AgainstNull(salesOrderItems, nameof(salesOrderItems));

            Name = name;
            ContactId = contactId;
            CorporateId = corporateId;
            QuotationId = quotationId;
            ShipToAddress = shipToAddress;
            _salesOrderItems = salesOrderItems;
        }

        public void AddItem(SalesOrderItem salesOrderItem)
        {
            Guard.AgainstNull(salesOrderItem, nameof(salesOrderItem));

            _salesOrderItems.Add(salesOrderItem);
        }

        public decimal Total()
        {
            decimal total = 0m;

            foreach (SalesOrderItem salesOrderItem in _salesOrderItems)
            {
                total += salesOrderItem.UnitPrice * salesOrderItem.Units;
            }

            return total;
        }
    }
}
