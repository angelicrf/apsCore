﻿using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace ConfriguringApps.Infrastructure
{
    public class BrowserTypeMiddleware
    {
        private RequestDelegate nextDelegate;
        public BrowserTypeMiddleware(RequestDelegate next) => nextDelegate = next;
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["EdgeBrowser"]
            = httpContext.Request.Headers["User-Agent"]
            .Any(v => v.ToLower().Contains("edge"));
            await nextDelegate.Invoke(httpContext);
        }
    }
}
