using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.CropProductionAggregate
{
    public class CropProductionField : BaseEntity
    {
        public Guid FieldId { get; private set; }

        public Field Field { get; private set; } = null!;

        public Guid CropProductionId { get; private set; }

        public CropProduction CropProduction { get; private set; } = null!;

        public CropProductionField(Guid fieldId, Guid cropProductionId)
        {
            Guard.AgainstNull(fieldId, nameof(fieldId));
            Guard.AgainstNull(cropProductionId, nameof(cropProductionId));

            FieldId = fieldId;
            CropProductionId = cropProductionId;
        }
    }
}
