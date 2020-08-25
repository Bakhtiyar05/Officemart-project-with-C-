﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public OfficeMartContext(DbContextOptions<OfficeMartContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderNumber> OrderNumbers { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL5063.site4now.net;Initial Catalog=DB_A66598_OfficeMart;User Id=DB_A66598_OfficeMart_admin;Password=V3272327v");
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColorConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductSizeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductImageConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderNumberConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SliderConfiguration).Assembly);
        }

    }
}
