using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class PriceListConfig : BaseEntityConfig<PriceList>
    {
        public override void Configure(EntityTypeBuilder<PriceList> builder)
        {
            base.Configure(builder);

            builder.Property(pl => pl.Name)
                .HasMaxLength(180)
                .IsRequired();

            builder.Property(pl => pl.UnitPrice)
                .HasColumnType("decimal(18,2)");
        }
    }
}
