using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        public async Task<IActionResult> List()
        {
            var NotApprovedOrders =await new OrdersLogic().GetNotApprovedOrders();
            return View(NotApprovedOrders);
        }
        public async Task<IActionResult> OrderDetail(int id)
        {
            var orderDetails = await new OrdersLogic().GetOrderDetails(id);
            return View(orderDetails);
        }
    }
}
