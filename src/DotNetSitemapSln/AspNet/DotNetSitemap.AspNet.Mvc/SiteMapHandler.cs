using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Linq;

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
            var lastModifiedDate = cacheProvider.GetLastModifiedDateUtc(cachePath);
            context.Response.AddHeader("Last-Modified", lastModifiedDate.ToString("r"));
            cacheProvider.WriteCacheToStream(cachePath, context.Response.OutputStream);
        }

        private void HandleRequest(string path, HttpContext context, ICacheProvider cacheProvider)
        {
            var generator = DotNetSitemapConfig.Container.Resolve<ISiteMapGenerator>();
            
            SitemapXml data = DotNetSitemapConfig.Option.GetData(path);
            if(data == null)
            {
                data = new SitemapXml();
            }
            if(path == DotNetSitemapConfig.Option.GetSitemapPath())
            {
                // If requests are send to sitemap.xml
                // then we need to consider to render SiteMapIndex
                var now = DateTimeOffset.UtcNow;
                var locs = DotNetSitemapConfig.Option.GetSitemapIndexLocs();
                foreach(var loc in locs)
                {
                    data.SitemapIndex.Sitemaps.Add(new Sitemap
                    {
                        Loc = loc,
                        LastMod = now
                    });
                }

            }
            var cachePath = Path.Combine(DotNetSitemapConfig.Option.Cache.Location, path);

            context.Response.Filter = cacheProvider.GetFilterStream(cachePath,
                context.Response.Filter,
                DotNetSitemapConfig.Option);

            generator.Render(context.Response.OutputStream, data, context.Request.Url);
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }
    }
}
