using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Filters
{
    /// <summary>
    /// 资源filter，在请求到达控制器处理请求之前，和请求处理完之后
    /// 这个特性可以做一些缓存，
    /// 
    /// 比如一些万年不变的view，可以直接缓存在这里啦 
    /// </summary>
    public class CustomResourceFilterAttribute : Attribute, IResourceFilter
    {
        private static Dictionary<string, IActionResult> CacheView = null;
        private ILogger<CustomResourceFilterAttribute> _logger = null;

        static CustomResourceFilterAttribute()
        {
            CacheView = new Dictionary<string, IActionResult>();
        }

        public CustomResourceFilterAttribute(ILogger<CustomResourceFilterAttribute> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 请求处理完成之后调用这个函数
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //使用Url将页面缓存起来
            string url = $"{context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}";
            if (!CacheView.Keys.Contains(url) || (CacheView.Keys.Contains(url) && CacheView[url] == null))
            {
                CacheView.Add(url, context.Result);
            }
        }

        /// <summary>
        /// 请求到实例化控制器之前，进行拦截 ,可以在这里做些缓存
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            //使用请求的url来取缓存
            string url = $"{context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}";
            if (CacheView.Keys.Contains(url) && CacheView[url] != null)
            {
                context.Result = CacheView[url];
            }
        }
    }
}
