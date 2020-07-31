﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(75);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.ImageName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.RegDate)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}