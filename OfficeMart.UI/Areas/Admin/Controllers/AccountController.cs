using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("__rommor__")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await new AccountLogic().AdminLogin(loginDto, _signInManager,_userManager);
                if (result.OperationIsSuccessfull)
                    return RedirectToAction("Index","Home");
                else
                    ModelState.AddModelError("", result.ErrorMessage);
            }
            return View(loginDto);
        }
    }
}
