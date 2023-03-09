using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class CorporateSpecification : BaseSpecification<Corporate>
    {
        public CorporateSpecification(int skip, int take, string? searchQuery)
            : base(c => string.IsNullOrEmpty(searchQuery) || c.Name.Contains(searchQuery))
        {
            ApplyPaging(skip, take);
        }
    }
}
