using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using System.IO;
using System.Web;
using System.Web.Routing;

namespace DotNetSitemap.AspNet
{
    public class SiteMapHandler : IHttpHandler, IRouteHandler
    {
        public bool IsReusable => true;
        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request.Url.AbsolutePath.Substring(1);
            var cacheProvider = DotNetSitemapConfig.Container.Resolve<ICacheProvider>();
            var cachePath = Path.Combine(DotNetSitemapConfig.Option.Cache.Location, path);

            context.Response.ContentType = "application/xml";

            if (DotNetSitemapConfig.Option.IsCacheable()
                && cacheProvider.IsCached(cachePath)
                && !cacheProvider.IsExpired(cachePath, DotNetSitemapConfig.Option))
            {
                HandleCache(cachePath, context, cacheProvider);
                return;
            }
            HandleRequest(path, context, cacheProvider);

        }

        private void HandleCache(string cachePath, HttpContext context, ICacheProvider cacheProvider)
        {
            cacheProvider.WriteCacheToStream(cachePath, context.Response.OutputStream);
        }

        private void HandleRequest(string path, HttpContext context, ICacheProvider cacheProvider)
        {
            var generator = DotNetSitemapConfig.Container.Resolve<ISiteMapGenerator>();

            var data = DotNetSitemapConfig.Option.GetData(path);
            var cachePath = Path.Combine(DotNetSitemapConfig.Option.Cache.Location, path);

            context.Response.Filter = cacheProvider.GetFilterStream(cachePath,
                context.Response.Filter,
                DotNetSitemapConfig.Option);

            generator.Render(context.Response.OutputStream, data);
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }
    }
}
