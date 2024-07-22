using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeMart.Domain.EntityConfigurations;
using OfficeMart.Domain.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OfficeMart.Domain.Models.AppDbContext
{
    public class OfficeMartContext : IdentityDbContext<AppUser>
    {
        public OfficeMartContext() { }

        public OfficeMartContext(DbContextOptions<OfficeMartContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<OrderPolicy> OrderPolicy { get; set; }
        public DbSet<RegisterPolicy> RegisterPolicy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=OFFICEMART;initial catalog=OfficeMartWebSite;user id=officemartweb;password=officemartweb;TrustServerCertificate=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SliderConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderPolicyConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegisterPolicyConfiguration).Assembly);
        }

    }
}
