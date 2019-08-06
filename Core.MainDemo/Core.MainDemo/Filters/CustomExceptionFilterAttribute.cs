using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Filters
{
    /// <summary>
    /// 异常filter
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        //private ILogger<CustomExceptionFilterAttribute> _logger = null;

        //public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        //{
        //    this._logger = logger;
        //}

        public CustomExceptionFilterAttribute()
        {
           
        }    


        public override void OnException(ExceptionContext context)
        {
            string controllerName = context.RouteData.Values["controller"].ToString();
            string routeName = context.RouteData.Values["route"].ToString();

            if (context.HttpContext.Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))//ajax请求
            {
                context.Result = new JsonResult(new
                {
                    Result = false,
                    PromptMsg = "系统出现异常！",
                    DeBugMessage = context.Exception.Message
                });
            }
            else
            {
                var result = new ViewResult() { ViewName = "~/Views/Shared/Error.cshtml", };
                result.ViewData["Exception"] = context.Exception.Message;
                context.Result = result;
            }
        }

    }
}
