using OfficeMart.Business.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OfficeMart.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using OfficeMart.Domain.Models.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OfficeMart.Business.Middlewares
{
    public static class AppMiddleware
    {
        public static void ConfigureMyServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<OfficeMartContext>(options => options.UseSqlServer("Data Source=SQL5063.site4now.net;Initial Catalog=DB_A66598_OfficeMart;User Id=DB_A66598_OfficeMart_admin;Password=V3272327v"));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<OfficeMartContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });
        }
    }
}
