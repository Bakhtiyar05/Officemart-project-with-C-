using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("AdminPage_pass--0201")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var statisticDto = await new AdminHomeLogic().GetAdminStatistic();
            return View(statisticDto);
        }
    }
}
