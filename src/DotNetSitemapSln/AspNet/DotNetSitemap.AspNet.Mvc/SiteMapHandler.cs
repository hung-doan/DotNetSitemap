using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Linq;
using System.Net;
using DotNetSitemap.Core.Models.MultipleSitemap;

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

            ISitemapData data = DotNetSitemapConfig.Option.GetData(path);

            if (data == null)
            {
                // Looking for data function, if there is no data function then return 404
                // If this is not sitemap.xml then return 404
                if (path != DotNetSitemapConfig.Option.GetSitemapPath())
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }

                // If this is sitemap.xml
                var siteMapIndex = new SitemapIndex();
                var locs = DotNetSitemapConfig.Option.GetSitemapIndexLocs();

                // Looking for sitemap index,
                // If there is no sitemap index then return 404
                if(!locs.Any())
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }

                foreach (var loc in locs)
                {
                    var siteMapItem = new Sitemap
                    {
                        Loc = loc
                    };

                    if (cacheProvider.IsCached(loc))
                    {
                        siteMapItem.LastMod = cacheProvider.GetLastModifiedDateUtc(loc);
                    }

                    siteMapIndex.Sitemaps.Add(siteMapItem);

                }

                data = siteMapIndex;
            }

            var cachePath = Path.Combine(DotNetSitemapConfig.Option.Cache.Location, path);

            context.Response.Filter = cacheProvider.GetFilterStream(cachePath,
                context.Response.Filter,
                DotNetSitemapConfig.Option);

            generator.Render(data, context.Response.OutputStream, context.Request.Url);
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }
    }
}
