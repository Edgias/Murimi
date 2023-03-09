using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class UnitMeasurement : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public Guid UnitGroupId { get; private set; }

        public UnitGroup UnitGroup { get; private set; } = null!;

        public UnitMeasurement(string name, Guid unitGroupId)
        {
            SetData(name, unitGroupId);
        }

        public void UpdateDetails(string name, Guid unitGroupId)
        {
            SetData(name, unitGroupId);
        }

        private void SetData(string name, Guid unitGroupId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(unitGroupId, nameof(unitGroupId));

            Name = name;
            UnitGroupId = unitGroupId;
        }
    }
}
