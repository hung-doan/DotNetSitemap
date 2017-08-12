using DotNetSitemap.AspNetCore.Filters;
using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetSitemap.AspNetCore
{
    public static class MvcApplicationBuilderExtensions
    {
        private static void MyDelegate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var sitemapService = DotNetSitemapConfig.Container.Resolve<ISitemapHttpContextService>();
                sitemapService.ProcessRequest(context);
                await context.Response.WriteAsync("Returning from Map");
            });
        }
        public static IApplicationBuilder UseDotNetSiteMap(this IApplicationBuilder app, DotNetSitemapOption options)
        {
            // Update container registration
            DotNetSitemapConfig.Container.Register<ISitemapGenerator, SitemapGenerator>();
            DotNetSitemapConfig.Container.Register<ICacheProvider, LocalFileCacheProvider>();
            DotNetSitemapConfig.Container.Register<ISitemapHttpContextService, SitemapHttpContextService>();

            // Route
            var registeredPaths = options.GetAllDataFuncPaths();

            // If there is no sitemap.xml registered yet, then register sitemap.xml
            var sitemapPath = options.GetSitemapPath();
            if (!registeredPaths.Any(p => p == sitemapPath))
            {
                app.Map($"/{sitemapPath}", MyDelegate);
            }

            foreach (var path in registeredPaths)
            {
                app.Map($"/{path}", MyDelegate);
            }

            return app;
        }
    }
}
