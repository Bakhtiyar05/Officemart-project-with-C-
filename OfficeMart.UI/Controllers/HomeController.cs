using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeMart.Domain.Models.AppDbContext;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.UI.Models.API;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static OfficeMart.UI.API.NetworkManager;

namespace OfficeMart.UI.Controllers
{
    public class HomeController : Controller
    {
        private const string AdminRoleName = "admin";
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        private readonly UserManager<AppUser> _userManager;
        private readonly OfficeMartContext _context;

        public HomeController(IConfiguration configuration,
                              RoleManager<IdentityRole> roleManager,
                              IPasswordHasher<AppUser> passwordHasher,
                              UserManager<AppUser> userManager,
                              OfficeMartContext context)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await EnsureAdminRoleAndUserAsync();

            var categoryData = await GetCategoriesAsync();
            var activeSliders = _context.Sliders.Where(s => s.Status).ToList();

            var viewModel = new HomeViewModel
            {
                categories = categoryData.Categories.Where(c => c.Status).ToList(),
                sliders = activeSliders
            };

            return View(viewModel);
        }

        private async Task EnsureAdminRoleAndUserAsync()
        {
            var adminPassword = _configuration["Admin:Password"];
            var adminUsername = _configuration["Admin:Username"];

            if (adminPassword == null || adminUsername == null)
            {
                throw new InvalidOperationException("Admin username or password is not configured.");
            }

            if (!await _roleManager.RoleExistsAsync(AdminRoleName))
                await _roleManager.CreateAsync(new IdentityRole(AdminRoleName));

            var adminUser = await _userManager.FindByNameAsync(adminUsername);
            if (adminUser == null)
            {
                var newUser = new AppUser
                {
                    UserName = adminUsername,
                    Name = "Admin",
                    Surname = "Admin",
                    IsAdmin = true
                };

                newUser.PasswordHash = _passwordHasher.HashPassword(newUser, adminPassword);

                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(newUser, AdminRoleName);
                else
                    throw new InvalidOperationException("Failed to create admin user.");
            }
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        private async Task<CategoryData> GetCategoriesAsync()
        {
            try
            {
                var response = await SendRequestAsync<CategoryData>("Category", _configuration, HttpMethod.Get);
                return response ?? new CategoryData();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error fetching category data.", ex);
            }
        }
    }
}
