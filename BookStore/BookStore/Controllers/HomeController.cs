using BookStore.Models;
using BookStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("[controller]/[action]")]

    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUserService _userService;

        public HomeController(IConfiguration _configuration,IUserService userService)
        {
            configuration = _configuration;
            _userService = userService;
        }
        [Route("~/")]
        public ViewResult Index()
        {
            var userid = _userService.GetUserId();
            var IsLoggedIn = _userService.IsAuthenticated();
            var newBookAlert =  new NewBookAlertConfig();
            configuration.Bind("NewBookALert", newBookAlert);
            bool displaybook = newBookAlert.DisplayNewBookAlert;

            return View();
        }

        public ViewResult AboutUs()
        {
            return View();
        }

        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
