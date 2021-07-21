using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepositiory _accountRepositiory;

        public AccountController(IAccountRepositiory accountRepositiory)
        {
            _accountRepositiory = accountRepositiory;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result =await _accountRepositiory.CreateUserAsync(userModel);

                if (!result.Succeeded)
                {
                    foreach (var errorMsg in result.Errors)
                    {
                        ModelState.AddModelError("", errorMsg.Description);
                    }
                    return View(userModel);
                }
                ModelState.Clear();
            }
            return View();
        }
    }
}
