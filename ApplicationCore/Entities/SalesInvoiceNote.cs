using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class SalesInvoiceNote : BaseEntity, IAggregateRoot
    {
        public string Description { get; private set; }

        public SalesInvoiceNote(string description)
        {
            Guard.AgainstNullOrEmpty(description, nameof(description));

            Description = description;
        }

        public void UpdateDetails(string description)
        {
            Guard.AgainstNullOrEmpty(description, nameof(description));

            Description = description;
        }

    }
}
