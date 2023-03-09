using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class SalesInvoiceNoteConfig : BaseEntityConfig<SalesInvoiceNote>
    {
        public override void Configure(EntityTypeBuilder<SalesInvoiceNote> builder)
        {
            base.Configure(builder);

            builder.Property(sin => sin.Description)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
