using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class SoilType : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public SoilType(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Name = name;
        }

        public void UpdateDetails(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Name = name;
        }

    }
}
