using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin__pass--0201")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
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
                var result = await new AccountLogic().Login(loginDto, _signInManager);
                if (result.OperationIsSuccessfull)
                    return RedirectToAction("Index","Home");
                else
                    ModelState.AddModelError("", result.ErrorMessage);
            }
            return View(loginDto);
        }
    }
}
