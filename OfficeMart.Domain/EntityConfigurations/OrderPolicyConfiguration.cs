using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.Domain.EntityConfigurations
{
	public class OrderPolicyConfiguration : IEntityTypeConfiguration<OrderPolicy>
    {
        public void Configure(EntityTypeBuilder<OrderPolicy> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

