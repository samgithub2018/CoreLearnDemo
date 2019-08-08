using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Middlewares
{
    public class LogMiddleware
    {
        public RequestDelegate _Next = null;
        private ILogger<LogMiddleware> _Logger = null;

        public LogMiddleware(RequestDelegate requestDelegate, ILogger<LogMiddleware> logger)
        {
            this._Next = requestDelegate;
            this._Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await Task.Run(() =>
            {
                this._Logger.LogTrace($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}： this {context.Request.Path.Value} Executing");
                this._Next(context);
                //this._Logger.LogTrace($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}： this {context.Request.Path.Value} Executed");
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}： this {context.Request.Path.Value} Executed");//这一句会执行的，看控制台虽然没有断点到
            });

        }


    }
}
