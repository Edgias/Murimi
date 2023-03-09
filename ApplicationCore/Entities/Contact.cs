using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class Contact : BaseEntity, IAggregateRoot
    {
        public ContactType ContactType { get; private set; }

        public string FirstName { get; private set; } = string.Empty;

        public string LastName { get; private set; } = string.Empty;

        public string? Email { get; private set; }

        public string? WorkPhone { get; private set; }

        public string? Mobile { get; private set; }

        public string? JobTitle { get; private set; }

        public Guid? SalutationId { get; private set; }

        public Salutation? Salutation { get; private set; }

        public Address? ContactAddress { get; private set; }

        private Contact()
        {
            // Required by EF
        }

        public Contact(ContactType contactType, string firstName, string lastName, string? email, string? workPhone,
            string? mobile, string? jobTitle, Guid salutationId, Address contactAddress)
        {
            Guard.AgainstNull(lastName, nameof(lastName));
            Guard.AgainstNull(salutationId, nameof(salutationId));

            ContactType = contactType;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            WorkPhone = workPhone;
            Mobile = mobile;
            SalutationId = salutationId;
            JobTitle = jobTitle;
            ContactAddress = contactAddress;
        }

        public void Update(string firstName, string lastName, string? email, string? workPhone,
            string? mobile, Guid salutationId, string? jobTitle, Address contactAddress)
        {
            Guard.AgainstNull(lastName, nameof(lastName));
            Guard.AgainstNull(salutationId, nameof(salutationId));

            FirstName = firstName;
            LastName = lastName;
            Email = email;
            WorkPhone = workPhone;
            Mobile = mobile;
            SalutationId = salutationId;
            JobTitle = jobTitle;
            ContactAddress = contactAddress;
        }
    }
}
