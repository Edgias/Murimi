using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class BinType : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public BinType(string name)
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
