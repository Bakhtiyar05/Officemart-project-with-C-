using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeMart.Business.Dtos;
using OfficeMart.Domain.Models.Entities;
using OfficeMart.UI.Models.API;
using Newtonsoft.Json;
using static OfficeMart.UI.API.NetworkManager;
using Microsoft.AspNetCore.Http;
using OfficeMart.Domain.Models.AppDbContext;

namespace OfficeMart.UI.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private readonly OfficeMartContext _context;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IWebHostEnvironment environment,
            IConfiguration configuration,
            OfficeMartContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _configuration = configuration;
            _context = context;
        }

        [Route("Daxil Ol_Qeydiyyat")]
        public IActionResult Index()
        {
            ViewBag.Policy = _context.RegisterPolicy.FirstOrDefault().Policy;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AppUserDto appUserDto)
        {
            if (ModelState.IsValid)
            {
                if (!appUserDto.IsPolicyAccepted)
                {
                    ModelState.AddModelError("", "Qeydiyyat şərtlərini oxuyun və qəbul edin.");
                    return View("Index");
                }
                string jsonBody = JsonConvert.SerializeObject(appUserDto);
                LoginResponse response = await RegisterUserAsync(jsonBody);
                if (response.Success)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };
                    Response.Cookies.Append("RememberMe", "false", cookieOptions);
                    Response.Cookies.Append("ClientCode", response.ClientCode, cookieOptions);
                    Response.Cookies.Append("Email", appUserDto.Email, cookieOptions);

                    return Redirect("/Home/Index");
                }
                else
                    ModelState.AddModelError("", "Qeydiyyat uğursuz oldu, zəhmət olmasa bütün sahələri doldurduğunuzdan əmin olduqdan sonra yenidən cəhd edin.");
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                string jsonBody = JsonConvert.SerializeObject(loginDto);
                LoginResponse response = await LoginUserAsync(jsonBody);
                if (response.Success)
                {
                    var cookieOptions = new CookieOptions
                    {
                        Expires = loginDto.RememberMe ? DateTimeOffset.Now.AddDays(365) : null,
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };
                    Response.Cookies.Append("RememberMe", loginDto.RememberMe.ToString(), cookieOptions);
                    Response.Cookies.Append("ClientCode", response.ClientCode, cookieOptions);
                    Response.Cookies.Append("Email", loginDto.Email, cookieOptions);

                    return Redirect("/Home/Index");
                }
                else
                    ModelState.AddModelError("", "Email və ya şifrəniz yanlışdır.");
            }
            return View("Index");
        }

        public IActionResult Signout()
        {
            Response.Cookies.Delete("RememberMe");
            Response.Cookies.Delete("ClientCode");
            Response.Cookies.Delete("Email");
            
            return Redirect("/Home/Index");
        }

        public async Task<IActionResult> SignOutForAdmin()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }

        private async Task<LoginResponse> LoginUserAsync(string jsonBody)
        {
            var loginAPIResponse = await SendRequestAsync<LoginResponse>("Login", _configuration, HttpMethod.Post, jsonBody);
            return loginAPIResponse ?? new LoginResponse();
        }

        private async Task<LoginResponse> RegisterUserAsync(string jsonBody)
        {
            var loginAPIResponse = await SendRequestAsync<LoginResponse>("Registration", _configuration, HttpMethod.Post, jsonBody);
            return loginAPIResponse ?? new LoginResponse();
        }
    }
}
