using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class SoilTypeSpecification : BaseSpecification<SoilType>
    {
        public SoilTypeSpecification(string searchQuery)
            : base(st => string.IsNullOrEmpty(searchQuery) || st.Name.Contains(searchQuery))
        {
        }

        public SoilTypeSpecification(int skip, int take, string searchQuery) 
            : base(st => string.IsNullOrEmpty(searchQuery) || st.Name.Contains(searchQuery))
        {
            ApplyOrderBy(st => st.Name);
            ApplyPaging(skip, take);
        }
    }
}
