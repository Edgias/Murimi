using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class OwnershipTypeSpecification : BaseSpecification<OwnershipType>
    {
        public OwnershipTypeSpecification(string searchQuery)
            : base(ot => string.IsNullOrEmpty(searchQuery) || ot.Name.Contains(searchQuery))
        {
        }

        public OwnershipTypeSpecification(int skip, int take, string searchQuery) 
            : base(ot => string.IsNullOrEmpty(searchQuery) || ot.Name.Contains(searchQuery))
        {
            ApplyOrderBy(ot => ot.Name);
            ApplyPaging(skip, take);
        }
    }
}
