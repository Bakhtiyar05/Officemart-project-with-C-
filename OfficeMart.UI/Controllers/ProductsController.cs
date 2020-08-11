using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await new ProductLogic().GetProducts();
            return View(products);
        }

        public async Task<IActionResult> CategoryProducts(int id)
        {
            var products = await new ProductLogic().GetCategoryProducts(id);
            return View(products);
        }
    }
}
