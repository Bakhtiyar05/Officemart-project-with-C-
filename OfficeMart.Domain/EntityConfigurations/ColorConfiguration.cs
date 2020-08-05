using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ColorName)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}
