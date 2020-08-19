using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> PendingOrders()
        {
            var userName = User.Identity.Name;
            var signedUser =await _userManager.FindByNameAsync(userName);


            var orders =await new PendingOrderLogic().GetUserPendingOrders(signedUser.Id, false);
            return View(orders);
        }

    }
}
