using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> ProductsList(int id, int page = 1)
        {
            var products = await new ProductLogic().GetProducts(page);
            return View(products);
        }

        public async Task<IActionResult> CategoryProducts(int id,int page = 1)
        {
            var products = await new ProductLogic().GetProductsPerPage(id,page);
            return View(products);
        }
    }
}
