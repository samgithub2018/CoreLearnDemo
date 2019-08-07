using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Filters
{
    /// <summary>
    /// action返回值filter
    /// </summary>
    public class CustomResultFilterAttribute : Attribute, IResultFilter
    {
        private ILogger<CustomResultFilterAttribute> _logger = null;

        public CustomResultFilterAttribute(ILogger<CustomResultFilterAttribute> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 返回值处理完成之后执行这个函数
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        /// <summary>
        /// 处理返回值之前执行这个函数
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {

        }
    }
}
