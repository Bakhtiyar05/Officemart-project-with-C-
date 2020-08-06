using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Controllers
{
    public class ProductsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await new ProductLogic().GetProducts();
            return View(products);
        }
    }
}
