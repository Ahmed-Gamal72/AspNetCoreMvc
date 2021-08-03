using BookStore.Models;
using BookStore.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepositiory : IAccountRepositiory
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AccountRepositiory(UserManager<ApplicationUser> userManager , 
            SignInManager<ApplicationUser> signInManager,
            IUserService userService , IEmailService emailService ,IConfiguration configuration )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName=userModel.FirstName,
                LasttName=userModel.LasttName,
                Email=userModel.Email,
                UserName=userModel.Email
            };


          var result= await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                
                    await GenerateEmailConfirmationTokenAsync(user);
                
            }
            return result;
        }


        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
          return  await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync (ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
           return await _userManager.ChangePasswordAsync(user,model.CurrentPassword,model.NewPassword);

        }


        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user ,string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain + confirmationLink , user.Id , token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);

        }


        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        //public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        //{
        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        await SendForgotPasswordEmail(user, token);
        //    }
        //}
    }

}
