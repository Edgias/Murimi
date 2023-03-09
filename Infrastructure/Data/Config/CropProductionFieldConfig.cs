using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities.CropProductionAggregate;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class CropProductionFieldConfig : BaseEntityConfig<CropProductionField>
    {
        public override void Configure(EntityTypeBuilder<CropProductionField> builder)
        {
            base.Configure(builder);

        }
    }
}
