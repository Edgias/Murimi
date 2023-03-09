using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities.QuotationAggregate;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class QuotationItemConfig : BaseEntityConfig<QuotationItem>
    {
        public override void Configure(EntityTypeBuilder<QuotationItem> builder)
        {
            base.Configure(builder);

            builder.Property(pii => pii.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.OwnsOne(qi => qi.ItemQuoted, iq =>
            {
                iq.WithOwner();

                iq.Property(i => i.ItemName)
                    .HasMaxLength(160)
                    .IsRequired();
            });
        }
    }
}
