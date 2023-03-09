using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Crop : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public Guid CropCategoryId { get; private set; }

        public CropCategory CropCategory { get; private set; } = null!;

        public Guid CropUnitId { get; private set; }

        public CropUnit CropUnit { get; private set; } = null!;

        public Crop(string name, Guid cropCategoryId, Guid cropUnitId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(cropCategoryId, nameof(cropCategoryId));
            Guard.AgainstNull(cropUnitId, nameof(cropUnitId));

            Name = name;
            CropCategoryId = cropCategoryId;
            CropUnitId = cropUnitId;
        }

        public void UpdateDetails(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));

            Name = name;
        }

        public void UpdateCropCategory(Guid cropCategoryId)
        {
            Guard.AgainstNull(cropCategoryId, nameof(cropCategoryId));

            CropCategoryId = cropCategoryId;
        }

        public void UpdateCropUnit(Guid cropUnitId)
        {
            Guard.AgainstNull(cropUnitId, nameof(cropUnitId));

            CropUnitId = cropUnitId;
        }
    }
}
