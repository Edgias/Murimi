using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class UnitGroup : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public UnitGroup(string name)
        {
            SetData(name);
        }

        public void UpdateDetails(string name)
        {
            SetData(name);
        }

        private void SetData(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));

            Name = name;
        }
    }
}
