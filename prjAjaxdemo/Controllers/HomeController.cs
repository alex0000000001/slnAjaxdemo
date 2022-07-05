using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using prjAjaxdemo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace prjAjaxdemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoContext db;
        public HomeController(ILogger<HomeController> logger, DemoContext demoContext)
        {
            _logger = logger;
            db = demoContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AjaxPost()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }


        public IActionResult HomeWorkRegister()
        {
            return View();
        }

        public IActionResult HomeWorkFetch()
        {
            return View();
        }

        public IActionResult Promise()
        {
            return View();
        }
        public IActionResult fetch()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
