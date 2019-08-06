using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Filters
{
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// action执行后
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// action执行前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

        }
        /// <summary>
        /// 返回值2
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {

        }

        /// <summary>
        /// 在Core中这个函数执行是在 OnActionExecuted之后的，。。
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {

        }

    }
}
