using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class CheckoutConfiguration : IEntityTypeConfiguration<Checkout>
    {
        public void Configure(EntityTypeBuilder<Checkout> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CheckoutNumber)
                .HasMaxLength(85)
                .IsRequired();

            builder.Property(x => x.OrderCount)
                .IsRequired();

            builder.Property(x => x.SaledPrice)
                .IsRequired();

            builder.Property(x => x.TotalPrice)
                .IsRequired();

        }
    }
}
