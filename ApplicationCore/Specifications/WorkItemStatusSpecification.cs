using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.ApplicationCore.Specifications
{
    public class WorkItemStatusSpecification : BaseSpecification<WorkItemStatus>
    {
        public WorkItemStatusSpecification(string searchQuery)
            : base(wis => string.IsNullOrEmpty(searchQuery))
        {
        }

        public WorkItemStatusSpecification(int skip, int take, string searchQuery) 
            : base(wis => string.IsNullOrEmpty(searchQuery))
        {
            ApplyOrderBy(wis => wis.Name);
            ApplyPaging(skip, take);
        }
    }
}
