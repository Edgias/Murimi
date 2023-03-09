using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Location : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public decimal? Latitude { get; private set; }

        public decimal? Longitude { get; private set; }

        public Address LocationAddress { get; private set; } = null!;

        public Location(string name, decimal? latitude, decimal? longitude, Address locationAddress)
        {
            SetData(name, latitude, longitude, locationAddress);
        }

        public void UpdateDetails(string name, decimal? latitude, decimal? longitude, Address locationAddress)
        {
            SetData(name, latitude, longitude, locationAddress);
        }

        public void SetData(string name, decimal? latitude, decimal? longitude, Address locationAddress)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(locationAddress, nameof(locationAddress));

            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            LocationAddress = locationAddress;
        }

        public void UpdateLatitudeAndLongitude(decimal latitude, decimal longitude)
        {
            Guard.AgainstZero(latitude, nameof(latitude));
            Guard.AgainstZero(longitude, nameof(longitude));

            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
