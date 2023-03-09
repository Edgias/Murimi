using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Supplier : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public string Phone { get; private set; } = string.Empty;

        public string? Email { get; private set; }

        public string? Website { get; private set; }

        public string ContactPerson { get; private set; } = string.Empty;

        public string? ContactPersonEmail { get; private set; }

        public string ContactPersonPhone { get; private set; } = string.Empty;

        public Address BillingAddress { get; private set; } = null!;

        public Supplier(string name, string phone, string? email, string? website, string contactPerson, 
            string? contactPersonEmail, string contactPersonPhone, Address billingAddress)
        {
            SetData(name, phone, email, website, contactPerson, contactPersonEmail, contactPersonPhone, billingAddress);
        }

        public void UpdateDetails(string name, string phone, string? email, string? website, string contactPerson,
            string? contactPersonEmail, string contactPersonPhone, Address billingAddress)
        {
            SetData(name, phone, email, website, contactPerson, contactPersonEmail, contactPersonPhone, billingAddress);
        }

        private void SetData(string name, string phone, string? email, string? website, 
            string contactPerson, string? contactPersonEmail, string contactPersonPhone, Address billingAddress)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(phone, nameof(phone));
            Guard.AgainstNullOrEmpty(contactPerson, nameof(contactPerson));
            Guard.AgainstNullOrEmpty(contactPersonPhone, nameof(contactPersonPhone));

            Name = name;
            Phone = phone;
            Email = email;
            Website = website;
            ContactPerson = contactPerson;
            ContactPersonEmail = contactPersonEmail;
            ContactPersonPhone = contactPersonPhone;
            BillingAddress = billingAddress;
        }
    }
}
