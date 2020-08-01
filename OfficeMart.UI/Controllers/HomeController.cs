using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeMart.UI.Models;
using System.Diagnostics;

namespace OfficeMart.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
