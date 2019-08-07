using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Core.MainDemo.Controllers
{
    public class TestActionController : Controller
    {
        public IActionResult Index()
        {
            //测试资源filter
            ViewData["Title"] = DateTime.Now.ToString("yyyyMMdd HHmmssms");
            return View();
        }
    }
}