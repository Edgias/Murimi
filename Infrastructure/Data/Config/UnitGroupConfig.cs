using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class UnitGroupConfig : BaseEntityConfig<UnitGroup>
    {
        public override void Configure(EntityTypeBuilder<UnitGroup> builder)
        {
            base.Configure(builder);

            builder.Property(ug => ug.Name)
                .HasMaxLength(160)
                .IsRequired();
        }
    }
}
