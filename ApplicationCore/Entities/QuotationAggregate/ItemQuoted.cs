using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate
{
    public class ItemQuoted // Value Object
    {
        public Guid ItemId { get; private set; }

        public string ItemName { get; private set; } = string.Empty;

        private ItemQuoted()
        {
            // Required by EF
        }

        public ItemQuoted(Guid itemId, string itemName)
        {
            Guard.AgainstNull(itemId, nameof(itemId));
            Guard.AgainstNullOrEmpty(itemName, nameof(itemName));

            ItemId = itemId;
            ItemName = itemName;
        }
    }
}
