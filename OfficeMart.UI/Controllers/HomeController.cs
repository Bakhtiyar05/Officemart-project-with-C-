using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using OfficeMart.Business.Logic;
using OfficeMart.UI.Models;
using System;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public HomeController(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await new HomeLogic().GetCategories();
            return View(categories);
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
    }
}
