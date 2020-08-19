using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeMart.UI.Areas.Admin.Components
{
    public class OrderListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var checkNumbers = await new OrdersLogic().GetCheckOutNumbersForNotification();
            return View(checkNumbers);
        }
    }
}
