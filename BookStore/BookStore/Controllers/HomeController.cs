using BookStore.Models;
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

        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        [Route("~/")]
        public ViewResult Index()
        {
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
