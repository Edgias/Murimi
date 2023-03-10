using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class ItemSpecification : BaseSpecification<Item>
    {
        public ItemSpecification()
            : base(criteria: null!)
        {
            AddInclude(i => i.ItemCategory);
            ApplyOrderBy(i => i.Name);
        }

        public ItemSpecification(Guid itemCategoryId)
            : base(i => i.ItemCategoryId == itemCategoryId)
        {
            ApplyOrderBy(i => i.Name);
        }

        public ItemSpecification(string searchQuery)
            : base(i => string.IsNullOrEmpty(searchQuery) || 
            i.Name.Contains(searchQuery))
        {
        }

        public ItemSpecification(int skip, int take, string searchQuery) 
            : base(i => string.IsNullOrEmpty(searchQuery) || 
            i.Name.Contains(searchQuery))
        {
            AddInclude(i => i.ItemCategory);
            ApplyOrderBy(i => i.Name);
            ApplyPaging(skip, take);
        }
    }
}
