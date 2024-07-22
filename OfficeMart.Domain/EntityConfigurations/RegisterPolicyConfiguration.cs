using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.Domain.EntityConfigurations
{
	public class RegisterPolicyConfiguration : IEntityTypeConfiguration<RegisterPolicy>
    {
        public void Configure(EntityTypeBuilder<RegisterPolicy> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

