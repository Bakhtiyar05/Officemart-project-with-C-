using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OfficeMart.UI.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
