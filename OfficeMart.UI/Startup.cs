using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OfficeMart.Business.Middlewares;
using OfficeMart.UI.Resources;
using System.Globalization;

namespace OfficeMart.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AppMiddleware.ConfigureMyServices(services,Configuration);
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<SharedViewLocalizer>();
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("az-Latn-AZ"),
                    new CultureInfo("ru-RU"),
                };

                options.DefaultRequestCulture = new RequestCulture("az-Latn-AZ");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //we will use this in production mod
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/404");
                app.UseStatusCodePagesWithReExecute("/404");
            }
            else
            {
                app.UseExceptionHandler("/404");
                app.UseStatusCodePagesWithReExecute("/404");
                app.UseHsts();
            }

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Product}/{action=Index}/{page=1}");         

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Products}/{action=CategoryProducts}/{id?}/{page?}/{search?}");
            });
        }
    }
}
