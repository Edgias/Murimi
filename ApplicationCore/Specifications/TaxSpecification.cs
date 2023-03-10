using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class TaxSpecification : BaseSpecification<Tax>
    {
        public TaxSpecification()
            : base(criteria: null!)
        {
        }

        public TaxSpecification(string searchQuery)
            : base(t => string.IsNullOrEmpty(searchQuery) || t.Name.Contains(searchQuery))
        {
        }

        public TaxSpecification(int skip, int take, string searchQuery) 
            : base(t => string.IsNullOrEmpty(searchQuery) || t.Name.Contains(searchQuery))
        {
            ApplyOrderBy(t => t.Name);
            ApplyPaging(skip, take);
        }
    }
}
