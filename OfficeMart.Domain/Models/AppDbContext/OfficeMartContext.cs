using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Domain.EntityConfigurations;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeMart.Domain.Models.AppDbContext
{
    public class OfficeMartContext : IdentityDbContext<AppUser>
    {
        public OfficeMartContext() { }
      
        public OfficeMartContext(DbContextOptions<OfficeMartContext> options) : base(options){}
       
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OfficeMart;Integrated Security=SSPI;");
            base.OnConfiguring(optionsBuilder);

        }
        //services.Configure<IdentityOptions>(options =>
        //    {
        //        options.Password.RequiredLength = 8;
        //        options.Password.RequireUppercase = true;
        //        options.Password.RequireLowercase = true;
        //        options.Password.RequireDigit = false;
        //        options.Password.RequireNonAlphanumeric = false;
        //        options.Password.RequiredUniqueChars = 5;
        //    });

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);

        }

    }
}
