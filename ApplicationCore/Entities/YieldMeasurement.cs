using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class YieldMeasurement : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public YieldMeasurement(string name)
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
