using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities.CropProductionAggregate
{
    public class CropProductionVariety : BaseEntity
    {
        public Guid CropProductionId { get; private set; }

        public CropProduction CropProduction { get; private set; } = null!;

        public Guid CropVarietyId { get; private set; }

        public CropVariety CropVariety { get; private set; } = null!;

        public CropProductionVariety(Guid cropProductionId, Guid cropVarientyId)
        {
            Guard.AgainstNull(cropProductionId, nameof(cropProductionId));
            Guard.AgainstNull(cropVarientyId, nameof(cropVarientyId));

            CropProductionId = cropProductionId;
            CropVarietyId = cropVarientyId;
        }
    }
}
