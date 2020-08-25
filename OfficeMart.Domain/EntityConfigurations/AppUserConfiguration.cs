using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(m => m.Name)
                    .IsRequired()
                   .HasMaxLength(75);

            builder.Property(m => m.Surname)
                    .IsRequired()
                   .HasMaxLength(75);

            builder.Property(m => m.LivingPlace)
                   .HasMaxLength(350);

            builder.Property(m => m.IsPasswordReset)
                .HasDefaultValue(false);

        }
    }
}
