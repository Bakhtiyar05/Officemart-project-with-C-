using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class CartController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Checkout(string ids,string counts)
        {
            var intIds = new BasketModelLogic().ConvertInt(ids);
            var intCounts = new BasketModelLogic().ConvertInt(counts);

            var basketProducts =await new BasketModelLogic().GetProducts(intIds, intCounts);
            return View(basketProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutOrders(string ids, string counts,CheckoutDto checkoutDto)
        {
            var intIds = new BasketModelLogic().ConvertInt(ids);
            var intCounts = new BasketModelLogic().ConvertInt(counts);

            var basketProducts = await new BasketModelLogic().GetProducts(intIds, intCounts);
            return View(basketProducts);
        }
    }
}
