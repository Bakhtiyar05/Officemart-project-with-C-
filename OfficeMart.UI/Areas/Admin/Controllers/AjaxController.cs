using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AjaxController : Controller
    {
        public async Task<IActionResult> SearchOrders(DateTime beginDate, DateTime endDate, string pattern,string routeValue)
        {
            var result = await new SearchingLogic().GetOrders(beginDate, endDate, pattern,routeValue);
            return Json(result);
        }
    }
}
