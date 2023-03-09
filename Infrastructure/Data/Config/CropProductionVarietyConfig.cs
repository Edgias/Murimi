using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities.CropProductionAggregate;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class CropProductionVarietyConfig : BaseEntityConfig<CropProductionVariety>
    {
        public override void Configure(EntityTypeBuilder<CropProductionVariety> builder)
        {
            base.Configure(builder);
        }
    }
}
