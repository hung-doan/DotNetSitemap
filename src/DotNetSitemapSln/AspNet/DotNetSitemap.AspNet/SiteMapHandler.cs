using DotNetSitemap.Core;
using DotNetSitemap.Core.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Linq;
using System.Net;
using DotNetSitemap.Core.Models.MultipleSitemap;
using DotNetSitemap.Core.Middlewares;
using DotNetSitemap.Core.Middlewares.Caches;
using DotNetSitemap.Core.Middlewares.Renders;

namespace DotNetSitemap.AspNet
{
    public class SitemapHandler : IHttpHandler, IRouteHandler
    {
        public bool IsReusable => false;

        /// <summary>
        /// This function to use MvcRoute in case of not using HttpHandlers
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }

        public void ProcessRequest(HttpContext context)
        {
            var middlewareContext = new MiddlewareContext()
            {
                OutputStream = context.Response.OutputStream,
                HttpDelegate = new HttpDelegate(context),
                RequestUrl = new RequestUrl
                {
                    Host = context.Request.Url.Host,
                    Port = context.Request.Url.Port,
                    Scheme = context.Request.Url.Scheme,
                    Path = context.Request.Url.AbsolutePath,
                    DataPath = context.Request.Url.AbsolutePath.Substring(1) // data path must not staring  with /
                }
            };
            context.Response.ContentType = "application/xml";

            var cacheMiddleware = DotNetSitemapConfig.Container.Resolve<ICacheProvider>() as ISitemapMiddleware;
            SitemapMiddlewareHandler.Add(cacheMiddleware);
            SitemapMiddlewareHandler.Add<SitemapGeneratorMiddleware>();

            SitemapMiddlewareHandler.Invoke(middlewareContext);
        }

    }
}
