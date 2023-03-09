using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class CropVariety : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public Guid CropId { get; private set; }

        public Crop Crop { get; private set; } = null!;

        public CropVariety(string name, Guid cropId)
        {
            Guard.AgainstNull(cropId, nameof(cropId));
            Guard.AgainstNullOrEmpty(name, nameof(name));

            Name = name;
            CropId = cropId;
        }

        public void UpdateDetails(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));

            Name = name;
        }
    }
}
