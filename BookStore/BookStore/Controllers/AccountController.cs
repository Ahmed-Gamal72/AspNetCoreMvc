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


        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }


        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
             var result=  await _accountRepositiory.PasswordSignInAsync(signInModel);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid credentials");
            }
            return View(signInModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepositiory.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

         [Route("change-password")]
        public  IActionResult ChangePassword()
        {
            
            return View();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result =await _accountRepositiory.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }

            }

            return View(model);
        }

    }

}
