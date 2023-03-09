using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.PurchaseOrderAggregate
{
    public class ItemOrdered // Value Object
    {
        public Guid ItemId { get; private set; }

        public string ItemName { get; private set; } = string.Empty;

        private ItemOrdered()
        {
            // Required by EF
        }

        public ItemOrdered(Guid itemId, string itemName)
        {
            Guard.AgainstNull(itemId, nameof(itemId));
            Guard.AgainstNullOrEmpty(itemName, nameof(itemName));

            ItemId = itemId;
            ItemName = itemName;
        }
    }
}
