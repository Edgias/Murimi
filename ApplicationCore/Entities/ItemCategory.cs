using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class ItemCategory : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public ItemCategory(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Name = name;
        }
    }
}
