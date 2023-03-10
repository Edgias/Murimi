using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.SalesOrderAggregate
{
    public class SalesOrderItem : BaseEntity
    {
        public ItemOrdered ItemOrdered { get; private set; } = null!;

        public decimal UnitPrice { get; private set; }

        public int Units { get; private set; }

        private SalesOrderItem()
        {
            // Required by EF
        }

        public SalesOrderItem(ItemOrdered itemOrdered, decimal unitPrice, int units)
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
