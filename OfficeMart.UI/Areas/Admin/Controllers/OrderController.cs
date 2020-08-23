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
        public async Task<IActionResult> NotApprovedOrdersList(string checkNumber, int page = 1)
        {
            if(checkNumber == null)
            {
                var notApprovedOrders = await new OrdersLogic().GetNotApprovedOrders(page);
                return View(notApprovedOrders);
            }
            else
            {
                var notApprovedOrders = await new OrdersLogic().GetNotApprovedOrdersByCheckNumber(checkNumber);
                return View(notApprovedOrders);
            }
        }

        public async Task<IActionResult> GetApprovedOrdersList(int page)
        {
            var approvedOrders = await new OrdersLogic().GetApprovedOrders(page);
            return View(approvedOrders);
        }
      
        public async Task<IActionResult> OrderDetail(int id, ResultDto result)
        {
            var orderDetails = await new OrdersLogic().GetOrderDetails(id);
            ViewBag.isApprovedSuccessful = result.IsSuccessful;
            ViewBag.isRejected = result.IsRejected;
            return View(orderDetails);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveOrder(int orderNumberId)
        {
            var result = new ResultDto();
            result.IsSuccessful = await new OrdersLogic().ApproveOrder(orderNumberId);
            return RedirectToAction(nameof(OrderDetail), result); 
        }

        [HttpPost]
        public async Task<IActionResult> RejectOrder(int orderNumberId)
        {
            var result = new ResultDto();
            result.IsRejected = await new OrdersLogic().RejectOrder(orderNumberId);
            return RedirectToAction(nameof(OrderDetail), result);
        }
    }
}
