using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class OrderNumberConfiguration : IEntityTypeConfiguration<OrderNumber>
    {
        public void Configure(EntityTypeBuilder<OrderNumber> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderCheckNumber)
                .HasMaxLength(85)
                .IsRequired();

            builder.Property(x => x.RegDate)
                .IsRequired()
                .HasDefaultValue(DateTime.Now.ToString("yyyy-MM-dd"));
            builder.Property(x => x.IsApproved)
               .IsRequired()
               .HasDefaultValue(false);
        }
    }
}
