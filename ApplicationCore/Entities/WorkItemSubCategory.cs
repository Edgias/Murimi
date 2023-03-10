using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class WorkItemSubCategory : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public Guid WorkItemCategoryId { get; private set; }

        public WorkItemCategory WorkItemCategory { get; private set; } = null!;

        public WorkItemSubCategory(string name, Guid workItemCategoryId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNull(workItemCategoryId, nameof(workItemCategoryId));

            Name = name;
            WorkItemCategoryId = workItemCategoryId;
        }

        public void UpdateDetails(string name)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));

            Name = name;
        }

        public void UpdateWorkItemCategory(Guid workItemCategoryId)
        {
            Guard.AgainstNull(workItemCategoryId, nameof(workItemCategoryId));

            WorkItemCategoryId = workItemCategoryId;
        }
    }
}
