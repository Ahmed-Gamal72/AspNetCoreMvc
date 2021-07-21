using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class AccountRepositiory : IAccountRepositiory
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepositiory(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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

            return result;
        }
    }

}
