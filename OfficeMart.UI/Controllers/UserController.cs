using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Orders()
        {
            var orderStatus = Request.Query["stat"].ToString();
            if (string.IsNullOrEmpty(orderStatus) || !orderStatus.Contains("false") && !orderStatus.Contains("true"))
            {
                return NotFound();
            }
            var signedUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var orders = await new PendingOrderLogic().GetUserPendingOrders(signedUser.Id, Convert.ToBoolean(orderStatus));
            ViewBag.stat = Convert.ToBoolean(orderStatus);
            return View(orders);
        }

    }
}
