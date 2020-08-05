using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration( AppUserDto appUserDto)
        {
            if(ModelState.IsValid)
                if(await new AccountLogic().RegistrationAppUser(appUserDto, _userManager, _signInManager))
                    return Redirect("/Home/Index");
                
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var result = await new AccountLogic().Login(loginDto, _signInManager);
                if (result.OperationIsSuccessfull)
                    return Redirect("/Home/Index");
                else
                    ModelState.AddModelError("LoginError", result.ErrorMessage);
            }
            return View("Index");
        }
    }
}
