﻿using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using System.Threading.Tasks;

namespace OfficeMart.UI.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Checkout(string ids,string counts)
        {
            var intIds = new CheckoutLogic().ConvertInt(ids);
            var intCounts = new CheckoutLogic().ConvertInt(counts);

            var basketProducts =await new CheckoutLogic().GetProducts(intIds, intCounts);
            return View(basketProducts);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string ids, string counts,OrderDto orderDto)
        {
            var result = await new CheckoutLogic().SaveOrders(ids, counts, orderDto);
            ViewBag.ProductsIsOrderd = result;
            return View();
        }
    }
}
