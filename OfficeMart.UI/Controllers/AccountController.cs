using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Logic;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;

namespace OfficeMart.UI.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
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


            return View("Index", appUserDto);
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

            var result = _userManager.PasswordHasher.VerifyHashedPassword(dbUser, dbUser.PasswordHash, passwordDto.Password);

            if (PasswordVerificationResult.Failed == result)
            {
                ModelState.AddModelError("", "Mövcud şifrə düzgün deyil");
                return View(passwordDto);
            }
            var hashedPassword = _userManager.PasswordHasher.HashPassword(dbUser, passwordDto.ConfPassword);
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
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                using (var context = TransactionConfig.AppDbContext)
                {
                    var userMail = context.Users.Where(m => m.UserName.ToLower() == email.ToLower()).FirstOrDefault();
                    if (userMail == null)
                    {
                        return Json(new { status = "400", message = "Email tapılmadı!" });
                    }
                    try
                    {
                        SmtpClient sc = new SmtpClient();
                        sc.Port = 587;
                        sc.Host = "smtp.gmail.com";
                        sc.EnableSsl = true;
                        sc.Credentials = new NetworkCredential("officemartbaku@gmail.com", "OfficeMart2020");
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("officemartbaku@gmail.com", "OfficeMart");
                        mail.To.Add(email.Trim());
                        mail.Subject = "Office Mart Şifrə Bərpası";
                        mail.IsBodyHtml = true;
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(Path.Combine(_environment.WebRootPath, "templates", "ResetPassword.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{email}", email.Trim()).Replace("{url}", $"https://localhost:44339/Account/ResetPasswordConfirm?email={email}");
                        mail.Body = body;
                        using (var smtpClient = new SmtpClient())
                        {
                            await sc.SendMailAsync(mail);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    userMail.IsPasswordReset = true;
                    userMail.PasswordResetDate = DateTime.Now;
                    context.Users.Update(userMail);
                    await context.SaveChangesAsync();
                }
                return Json(new { status = "200" });

            }
            catch (Exception)
            {
                return Json(new { status = "400", message = "Email tapılmadı!" });

            }

        }
        [HttpGet]
        public IActionResult ResetPasswordConfirm(string email)
        {
            try
            {
                MailAddress address = new MailAddress(email);
                using (var context = TransactionConfig.AppDbContext)
                {
                    var userMail = context.Users.Where(m => m.UserName.ToLower() == email.ToLower()).FirstOrDefault();
                    if (userMail == null)
                    {
                        return NotFound();
                    }
                    if (!userMail.IsPasswordReset || userMail.PasswordResetDate.AddHours(24) < DateTime.Now)
                    {
                        return NotFound();
                    }
                    return View(new EmailPassResentDto { email = email });
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(EmailPassResentDto resentDto)
        {
            try
            {
                MailAddress address = new MailAddress(resentDto.email);
                using (var context = TransactionConfig.AppDbContext)
                {
                    var userMail = context.Users.Where(m => m.UserName.ToLower() == resentDto.email.ToLower()).FirstOrDefault();
                    if (userMail == null)
                    {
                        return NotFound();
                    }
                    if (!userMail.IsPasswordReset || userMail.PasswordResetDate.AddHours(24) < DateTime.Now)
                    {
                        return NotFound();
                    }
                    var hashedPassword = _userManager.PasswordHasher.HashPassword(userMail, resentDto.ConfPassword);
                    userMail.PasswordHash = hashedPassword;
                    userMail.IsPasswordReset = false;
                    userMail.PasswordResetDate = DateTime.Now.AddHours(-24);
                    context.Users.Update(userMail);
                    await context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
