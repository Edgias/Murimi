using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.PurchaseOrderAggregate
{
    public class PurchaseOrderItem : BaseEntity
    {
        public ItemOrdered ItemOrdered { get; private set; } = null!;

        public decimal UnitPrice { get; private set; }

        public int Units { get; private set; }

        private PurchaseOrderItem()
        {
            // Required by EF
        }

        public PurchaseOrderItem(ItemOrdered itemOrdered, decimal unitPrice, int units)
        {
            Guard.AgainstZero(units, nameof(units));
            Guard.AgainstZero(unitPrice, nameof(unitPrice));
            Guard.AgainstNull(itemOrdered, nameof(itemOrdered));

            ItemOrdered = itemOrdered;
            UnitPrice = unitPrice;
            Units = units;
        }
    }
}
