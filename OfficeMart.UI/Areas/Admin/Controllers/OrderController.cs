using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        public async Task<IActionResult> List(string id)
        {
            if(id == null)
            {
                var notApprovedOrders = await new OrdersLogic().GetNotApprovedOrders();
                return View(notApprovedOrders);
            }
            else
            {
                var notApprovedOrders = await new OrdersLogic().GetNotApprovedOrdersByCheckNumber(id);
                return View(notApprovedOrders);
            }
            
            
        }
      
        public async Task<IActionResult> OrderDetail(int id, ResultDto result)
        {
            var orderDetails = await new OrdersLogic().GetOrderDetails(id);
            ViewBag.isApprovedSuccessful = result.IsSuccessful;
            return View(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveOrder(int orderNumberId)
        {
            var result = new ResultDto();
            result.IsSuccessful = await new OrdersLogic().ApproveOrder(orderNumberId);
            return RedirectToAction(nameof(OrderDetail), result); 
        }
    }
}
