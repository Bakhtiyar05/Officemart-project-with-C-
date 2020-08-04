using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var productsDto = await new HomeLogic().GetProducts();
            return View(productsDto);
        }

    }
}
