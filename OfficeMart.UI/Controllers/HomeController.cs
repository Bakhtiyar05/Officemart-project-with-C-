using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.UI.Models;
using System.Collections.Generic;
using System.Diagnostics;
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
