using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class TaxConfig : BaseEntityConfig<Tax>
    {
        public override void Configure(EntityTypeBuilder<Tax> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(t => t.Percentage)
                .HasColumnType("decimal(18,2)");
        }
    }
}
