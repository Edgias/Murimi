using NigTech.Murimi.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class WorkItemSubCategoryConfig : BaseEntityConfig<WorkItemSubCategory>
    {
        public override void Configure(EntityTypeBuilder<WorkItemSubCategory> builder)
        {
            base.Configure(builder);

            builder.Property(wisc => wisc.Name)
                .HasMaxLength(180)
                .IsRequired();
        }
    }
}
