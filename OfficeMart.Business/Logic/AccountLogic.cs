using Microsoft.AspNetCore.Identity;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class AccountLogic
    {
        public async Task<bool> RegistrationAppUser(AppUserDto appUserDto,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            var appUser = new AppUser
            {
                Name = appUserDto.Name,
                Surname = appUserDto.Surname,
                UserName = appUserDto.Email,
                LivingPlace = appUserDto.LivinPlace,
                PhoneNumber = appUserDto.PhoneNumber
            };

            var userResult = await userManager.CreateAsync(appUser, appUserDto.Password);

            if (userResult.Succeeded)
            {
                var result = await signInManager.PasswordSignInAsync(appUserDto.Email, appUserDto.Password, true , false);
                if (result.Succeeded)
                    return true;
            }

            return false;
        }

        public async Task<LogicResult> Login(LoginDto loginDto,SignInManager<AppUser> signInManager)
        {
            var logicResult = new LogicResult();
            var result = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, true, false);

            if (result.Succeeded)
                logicResult.OperationIsSuccessfull = true;
            else
            {
                logicResult.OperationIsSuccessfull = false;
                logicResult.ErrorMessage = "Email yaxud şifrəniz yanlışdır";
            }

            return logicResult;
        }
    }
}
