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
        public async Task<LogicResult> AdminLogin(LoginDto loginDto, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
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
                else if (PasswordVerificationResult.Success == checkPasswordAdmin)
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
