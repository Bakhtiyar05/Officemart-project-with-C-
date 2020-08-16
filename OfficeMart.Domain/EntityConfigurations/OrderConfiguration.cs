using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderCount)
                .IsRequired();

            builder.Property(x => x.SaledPrice)
                .IsRequired();

            builder.Property(x => x.TotalPrice)
                .IsRequired();

            builder.Property(x => x.BuyerName)
                .HasMaxLength(85);

            builder.Property(x => x.BuyerSurname)
               .HasMaxLength(85);

            builder.Property(x => x.BuyerPhone)
               .IsRequired()
               .HasMaxLength(85);

            builder.Property(x => x.DeliveryAddress)
               .IsRequired()
               .HasMaxLength(600);

            builder.Property(x=>x.RegDate)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
    }
}
