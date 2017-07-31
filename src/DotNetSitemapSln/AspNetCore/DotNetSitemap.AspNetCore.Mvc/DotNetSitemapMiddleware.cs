using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSitemap.AspNetCore.Mvc
{
    public class DotNetSitemapMiddleware
    {
        private readonly RequestDelegate _next;

        public DotNetSitemapMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if(false)
            {
                return null;
            }
            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
