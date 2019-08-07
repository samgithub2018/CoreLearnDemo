using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.MainDemo.Models;
using Microsoft.Extensions.Logging;
using Core.MainDemo.Filters;

namespace Core.MainDemo.Controllers
{
    public class HomeController : Controller
    {
        public ILoggerFactory _lFactory { get; set; }
        public ILogger<HomeController> _logger { get; set; }
        public HomeController(ILoggerFactory lFactory, ILogger<HomeController> logger)
        {
            this._lFactory = lFactory;
            this._logger = logger;
        }

        //[ServiceFilter(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index()
        {
            // throw new Exception("自定义错误!");
            _logger.LogError("this HomeController");
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
