using DotNetSitemap.Core;
using DotNetSitemap.Core.Middlewares;
using DotNetSitemap.Core.Middlewares.Caches;
using DotNetSitemap.Core.Middlewares.Renders;
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
  
        private static void SitemapDelegate(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var middlewareContext = new MiddlewareContext()
                {
                    OutputStream = context.Response.Body,
                    HttpDelegate = new HttpDelegate(context),
                    RequestUrl = new RequestUrl
                    {
                        Host = context.Request.Host.Host,
                        Port = context.Request.Host.Port.Value,
                        Scheme = context.Request.Scheme,
                        Path = context.Request.PathBase.Value,
                        DataPath = context.Request.PathBase.Value.Substring(1) // data path must not staring  with /
                    }
                };
                context.Response.ContentType = "application/xml";

                var cacheMiddleware = DotNetSitemapConfig.Container.Resolve<ICacheProvider>() as ISitemapMiddleware;
                SitemapMiddlewareHandler.Add(cacheMiddleware);
                SitemapMiddlewareHandler.Add<SitemapGeneratorMiddleware>();

                SitemapMiddlewareHandler.Invoke(middlewareContext);


            });
        }
        public static IApplicationBuilder UseDotNetSiteMap(this IApplicationBuilder app, Action<IDotNetSitemapOption> optionAct)
        {
            var options = DotNetSitemapConfig.Container.Resolve<IDotNetSitemapOption>();
            optionAct.Invoke(options);

            // Route
            var registeredPaths = options.GetSitemapIndexPaths();

            // If there is no sitemap.xml registered yet, then register sitemap.xml
            var sitemapPath = options.SitemapPath;
            if (!registeredPaths.Any(p => p == sitemapPath))
            {
                app.Map($"/{sitemapPath}", SitemapDelegate);
            }

            foreach (var path in registeredPaths)
            {
                app.Map($"/{path}", SitemapDelegate);
            }

            DotNetSitemapConfig.SetOption(options);
            return app;
        }

        
    }
}
