using NigTech.Murimi.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class WorkItemCategoryConfig : BaseEntityConfig<WorkItemCategory>
    {
        public override void Configure(EntityTypeBuilder<WorkItemCategory> builder)
        {
            base.Configure(builder);

            builder.Property(wic => wic.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasIndex(wic => wic.Name)
                .IsUnique();
        }
    }
}
