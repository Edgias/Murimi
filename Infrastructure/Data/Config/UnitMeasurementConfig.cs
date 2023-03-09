﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NigTech.Murimi.ApplicationCore.Entities;

namespace NigTech.Murimi.Infrastructure.Data.Config
{
    internal class UnitMeasurementConfig : BaseEntityConfig<UnitMeasurement>
    {
        public override void Configure(EntityTypeBuilder<UnitMeasurement> builder)
        {
            base.Configure(builder);

            builder.Property(um => um.Name)
                .HasMaxLength(180)
                .IsRequired();
        }
    }
}
