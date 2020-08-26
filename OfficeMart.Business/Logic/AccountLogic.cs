using Microsoft.AspNetCore.Identity;
using OfficeMart.Business.Dtos;
using OfficeMart.Business.Models;
using OfficeMart.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeMart.Business.Logic
{
    public class AccountLogic
    {
        public async Task<AppUserDto> RegistrationAppUser(AppUserDto appUserDto,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            appUserDto.LogicResult = new LogicResult();
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
                {
                    appUserDto.LogicResult.OperationIsSuccessfull = true;
                    return appUserDto;
                }  
            }

            if(userResult.Errors.Count() != 0)
            {
                var isDuplicate = userResult.Errors.Where(x => x.Code == "DuplicateUserName").FirstOrDefault();
                appUserDto.LogicResult.ErrorMessage = isDuplicate != null ? "Email artıq istifadə olunur" : null;
                return appUserDto;
            }
            return appUserDto;
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

        public async Task<LogicResult> AdminLogin(LoginDto loginDto, SignInManager<AppUser> signInManager,UserManager<AppUser> userManager)
        {
            var logicResult = new LogicResult();

            var adminUser = await userManager.FindByNameAsync(loginDto.Email);

            if (adminUser != null && adminUser.IsAdmin == true)
            {
                var checkPasswordAdmin = userManager.PasswordHasher.VerifyHashedPassword(adminUser, adminUser.PasswordHash, loginDto.Password);
                if (PasswordVerificationResult.Failed == checkPasswordAdmin)
                {
                    logicResult.OperationIsSuccessfull = false;
                    logicResult.ErrorMessage = "Email yaxud şifrəniz yanlışdır";
                    return logicResult;
                }
                else if(PasswordVerificationResult.Success == checkPasswordAdmin)
                {
                    var result = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, true, false);

                    if (result.Succeeded)
                        logicResult.OperationIsSuccessfull = true;

                    return logicResult;
                }
            }

            throw new Exception("Not Admin");
        }
    }
}
