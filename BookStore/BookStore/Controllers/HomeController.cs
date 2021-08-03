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
        private readonly IEmailService _emailService;

        public HomeController(IConfiguration _configuration,IUserService userService,IEmailService emailService)
        {
            configuration = _configuration;
            _userService = userService;
            _emailService = emailService;
        }
        [Route("~/")]
        public async Task<ViewResult> Index()
        {
            //UserEmailOptions options = new UserEmailOptions
            //{
            //    ToEmails = new List<string>() { "test@gmail.com" },
            //    PlaceHolders =new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Ahmed")
            //    }
            //};

            //await _emailService.SendTestEmail(options);
           

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
