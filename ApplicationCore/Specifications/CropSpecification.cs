using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class CropSpecification : BaseSpecification<Crop>
    {
        public CropSpecification(string searchQuery)
            : base(c => string.IsNullOrEmpty(searchQuery) || c.Name.Contains(searchQuery))
        {
        }

        public CropSpecification(int skip, int take, string searchQuery) 
            : base(c => string.IsNullOrEmpty(searchQuery) || c.Name.Contains(searchQuery))
        {
            AddInclude(c => c.CropCategory);
            AddInclude(c => c.CropUnit);
            ApplyOrderBy(c => c.Name);
            ApplyPaging(skip, take);
        }
    }
}
