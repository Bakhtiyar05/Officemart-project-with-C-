using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class ProductImageConfiguration:IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.ImageName)
                .IsRequired();
            builder.Property(x => x.RegDate)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }

        
    }
}
