using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Domain.EntityConfigurations;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.AppDbContext
{
    public class OfficeMartContext : IdentityDbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OfficeMart;Integrated Security=SSPI;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        }
    }
}
