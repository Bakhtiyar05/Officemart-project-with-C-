using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {
            var categoryProducts = await new ProductLogic().GetProductsForDetailPage(id);
            return View(categoryProducts);
        }
    }
}
