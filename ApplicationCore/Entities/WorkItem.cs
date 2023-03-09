using NigTech.Murimi.ApplicationCore.Entities.CropProductionAggregate;
using NigTech.Murimi.ApplicationCore.SharedKernel;

namespace NigTech.Murimi.ApplicationCore.Entities
{
    public class WorkItem : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public DateTimeOffset? StartDate { get; private set; }

        public DateTimeOffset? EndDate { get; private set; }

        public Guid WorkItemStatusId { get; private set; }

        public WorkItemStatus WorkItemStatus { get; private set; } = null!;

        public Guid WorkItemCategoryId { get; private set; }

        public WorkItemCategory WorkItemCategory { get; private set; } = null!;

        public Guid? WorkItemSubCategoryId { get; private set; }

        public WorkItemSubCategory? WorkItemSubCategory { get; private set; }

        public Guid? FieldId { get; private set; }

        public Field? Field { get; private set; }

        public Guid SeasonId { get; private set; }

        public Season Season { get; private set; } = null!;

        public Guid? CropProductionId { get; private set; }

        public CropProduction? CropProduction { get; private set; }

        public WorkItem(string name, string description, DateTimeOffset? startDate, 
            DateTimeOffset? endDate, Guid workItemStatusId, Guid workItemCategoryId, 
            Guid? workItemSubCategoryId, Guid? fieldId, Guid seasonId, Guid? cropProductionId)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(description, nameof(description));
            Guard.AgainstNull(workItemStatusId, nameof(workItemStatusId));
            Guard.AgainstNull(workItemCategoryId, nameof(workItemCategoryId));
            Guard.AgainstNull(seasonId, nameof(seasonId));

            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            WorkItemStatusId = workItemStatusId;
            WorkItemCategoryId = workItemCategoryId;
            WorkItemSubCategoryId = workItemSubCategoryId;
            FieldId = fieldId;
            SeasonId = seasonId;
            CropProductionId = cropProductionId;

        }

        public void UpdateDetails(string name, string description, DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(description, nameof(description));

            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
        }

        public void UpdateCropProduction(Guid cropProductionId)
        {
            CropProductionId = cropProductionId;
        }

        public void UpdateField(Guid fieldId)
        {
            FieldId = fieldId;
        }

        public void UpdateWorkItemCategory(Guid workItemCategoryId)
        {
            WorkItemCategoryId = workItemCategoryId;
        }

        public void UpdateWorkItemSubCategory(Guid workItemSubCategoryId)
        {
            WorkItemSubCategoryId = workItemSubCategoryId;
        }

        public void UpdateWorkItemStatus(Guid workItemStatusId)
        {
            WorkItemStatusId = workItemStatusId;
        }
    }
}
