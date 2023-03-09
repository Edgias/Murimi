using NigTech.Murimi.ApplicationCore.Entities;
using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class ContactSpecification : BaseSpecification<Contact>
    {
        public ContactSpecification(ContactType contactType) 
            : base(c => c.ContactType == contactType)
        {
            AddInclude(c => c.Salutation!);
            ApplyOrderBy(c => c.LastName);
        }

        public ContactSpecification(int skip, int take, string? searchQuery)
            : base(c => !string.IsNullOrEmpty(searchQuery) || 
            c.FirstName.Contains(searchQuery!) || 
            c.LastName.Contains(searchQuery!) ||
            c.Mobile!.Contains(searchQuery!) ||
            c.Email!.Contains(searchQuery!))
        {
            ApplyOrderBy(c => c.LastName);
            ApplyPaging(skip, take);
        }
    }
}
