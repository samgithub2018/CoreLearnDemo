﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Core.MainDemo.Controllers
{
    public class AutofacController : Controller
    {

        private Interface1 _Interface1 = null;
        private Interface2 _Interface2 = null;
        private Interface3 _Interface3 = null;

        /// <summary>
        /// 通过autofac 注入，自定扩展的实例
        /// </summary>
        /// <param name="interface1"></param>
        /// <param name="interface2"></param>
        /// <param name="interface3"></param>
        public AutofacController(Interface1 interface1, Interface2 interface2, Interface3 interface3)
        {
            this._Interface1 = interface1;
            this._Interface2 = interface2;
            this._Interface3 = interface3;
        }

        public IActionResult Index()
        {
            this._Interface1.Show();
            this._Interface2.Show();
            this._Interface3.Show();
            return View();
        }
    }
}