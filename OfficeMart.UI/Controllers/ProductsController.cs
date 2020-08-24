using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class ProductsController : Controller
    {
        [Route("Məhsullarımız")]
        public async Task<IActionResult> ProductsList(int id, int page = 1, string search = "")
        {
            List<ProductDto> products;
            if (string.IsNullOrEmpty(search))
            {
                products = await new ProductLogic().GetProducts(page);
            }
            else
            {
                products = await new ProductLogic().GetSearchResult(page,search.ToLower());
            }
            return View(products);
        }

        [Route("Kateqoriya_Məhsulları")]
        public async Task<IActionResult> CategoryProducts(int id, int page = 1)
        {
            var products = await new ProductLogic().GetProductsPerPage(id, page);
            return View(products);
        }
    }
}
