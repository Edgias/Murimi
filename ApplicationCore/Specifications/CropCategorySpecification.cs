using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class CropCategorySpecification : BaseSpecification<CropCategory>
    {
        public CropCategorySpecification(string searchQuery)
            : base(cc => string.IsNullOrEmpty(searchQuery) || cc.Name.Contains(searchQuery) || cc.Description.Contains(searchQuery))
        {
        }

        public CropCategorySpecification(int skip, int take, string searchQuery) 
            : base(cc => string.IsNullOrEmpty(searchQuery) || cc.Name.Contains(searchQuery) || cc.Description.Contains(searchQuery))
        {
            ApplyOrderBy(cc => cc.Name);
            ApplyPaging(skip, take);
        }
    }
}
