using Microsoft.AspNetCore.Mvc;

namespace OfficeMart.UI.Controllers
{
    public class CategoryListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
