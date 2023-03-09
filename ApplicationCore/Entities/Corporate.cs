using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Corporate : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public string? Email { get; private set; }

        public string? Phone { get; private set; }

        public string? Website { get; private set; }

        public Address? CorporateAddress { get; private set; }

        private Corporate()
        {

        }

        public Corporate(string name, string? email, string? phone, string? website,
            Address corporateAddres)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Website = website;
            CorporateAddress = corporateAddres;
        }

        public void Update(string name, string? email, string? phone, string? website,
            Address corporateAddres)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Website = website;
            CorporateAddress = corporateAddres;
        }
}
}
