using DotNetSitemap.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSitemap.AspNetCore
{
    internal class DotNetSitemapMiddleware
    {
        public static void MapSitemap(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                
                await context.Response.WriteAsync("Returning from Map");
            });
        }

        public static void MapSitemapIndex(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Returning from Map");
            });
        }
    }
}
