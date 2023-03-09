using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Item : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public Guid ItemCategoryId { get; private set; }

        public ItemCategory ItemCategory { get; private set; } = null!;

        public Item(string name, string description, Guid itemCategoryId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(itemCategoryId, nameof(itemCategoryId));

            Name = name;
            Description = description;
            ItemCategoryId = itemCategoryId;
        }
    }
}
