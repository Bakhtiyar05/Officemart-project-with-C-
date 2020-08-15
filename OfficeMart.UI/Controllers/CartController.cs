using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;

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
    }
}
