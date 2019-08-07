using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Filters
{
    public class CustomIActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomIActionFilterAttribute> _logger = null;

        public CustomIActionFilterAttribute(ILogger<CustomIActionFilterAttribute> logger)
        {
            this._logger = logger;
        }


        /// <summary>
        /// action执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("**************this OnActionExecuting Method*****************");
        }

        /// <summary>
        /// action执行完成后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("**************this OnActionExecuted Method*****************");
        }

    }
}
