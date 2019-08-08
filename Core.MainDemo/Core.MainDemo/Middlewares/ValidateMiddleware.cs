using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Middlewares
{
    public class ValidateMiddleware
    {
        public RequestDelegate _Next = null;
        private ILogger<ValidateMiddleware> _Logger = null;

        public ValidateMiddleware(RequestDelegate requestDelegate, ILogger<ValidateMiddleware> logger)
        {
            this._Next = requestDelegate;
            this._Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (string.IsNullOrEmpty(context.Request.Path.Value))
            {
                await context.Response.WriteAsync("context.Request.Path.Value is null");
            }
            await this._Next(context);

        }
    }
}
