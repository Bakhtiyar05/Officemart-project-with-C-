using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Route("Daxil Ol_Qeydiyyat")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AppUserDto appUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await new AccountLogic().RegistrationAppUser(appUserDto, _userManager, _signInManager);
                if (appUserDto.LogicResult.OperationIsSuccessfull)
                    return Redirect("/Home/Index");
                else
                    ModelState.AddModelError("", result.LogicResult.ErrorMessage);
            }


            return View("Index",appUserDto);
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
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
        [Authorize]
        public async Task<IActionResult> Restore()
        {
            var tempUserName = User.Identity.Name;
            var dbUser = await _userManager.FindByNameAsync(tempUserName);
            return View(new RestorePasswordDto { PhoneNumber = dbUser.PhoneNumber });
        }
        [HttpPost]
        public async Task<IActionResult> Restore(RestorePasswordDto passwordDto)
        {
            if (!ModelState.IsValid)
            {
                return View(passwordDto);
            }

            var tempUserName = User.Identity.Name;
            var dbUser = await _userManager.FindByNameAsync(tempUserName);

            var result = _userManager.PasswordHasher.VerifyHashedPassword(dbUser,dbUser.PasswordHash,passwordDto.Password);

            if (PasswordVerificationResult.Failed == result)
            {
                ModelState.AddModelError("","Mövcud şifrə düzgün deyil");
                return View(passwordDto);
            }
            var hashedPassword = _userManager.PasswordHasher.HashPassword(dbUser,passwordDto.ConfPassword);
            dbUser.PasswordHash = hashedPassword;
            dbUser.PhoneNumber = passwordDto.PhoneNumber;
            var updResult = await _userManager.UpdateAsync(dbUser);
            if (updResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Əməliyyat uğursuz oldu");
                return View(passwordDto);
            }
        }
    }
}
