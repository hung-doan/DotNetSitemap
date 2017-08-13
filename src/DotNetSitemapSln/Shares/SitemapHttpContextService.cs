using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Models;
using DotNetSitemap.Core.Models.MultipleSitemap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using DotNetSitemap.Core.Models.SitemapOptions;
#if SITEMAP_ASPNET
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif

namespace DotNetSitemap.Core.Services
{
    public class SitemapHttpContextService : ISitemapHttpContextService
    {


        public void ProcessRequest(HttpContext context)
        {
            var options = DotNetSitemapConfig.Option;

            // Get relative path
            // Relative path must not staring  with /
#if SITEMAP_ASPNET
            var path = context.Request.Url.AbsolutePath.Substring(1);
#else
            var path = context.Request.PathBase.Value.Substring(1);
#endif

            var cacheProvider = DotNetSitemapConfig.Container.Resolve<ICacheProvider>();


            context.Response.ContentType = "application/xml";

            // If current request is cachable 
            // and It was cached
            // and It is not expired
            // then we return the cached version
            if (options.Cacheable)
            {
                var cachePath = Path.Combine(options.CacheLocation, path);
                if (cacheProvider.IsCached(cachePath)
                    && !cacheProvider.IsExpired(cachePath, options))
                {
                    HandleCache(cachePath, context, cacheProvider);
                    return;
                }

                // Set cache handler
                context.Response.Filter = cacheProvider.GetFilterStream(path,
               context.Response.Filter,
               options);
            }

            // If current request is not cachable
            // then there are 2 cases to handle
            // Handle sitemap.xml, 
            // Handle Other request: sitemapindex pages 

            var data = GetSitemapData(path, cacheProvider);
            if (data == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            // Then render the page with data
            var generator = DotNetSitemapConfig.Container.Resolve<ISitemapGenerator>();

#if SITEMAP_ASPNET

            generator.Render(data, context.Response.OutputStream, new RequestUrl()
            {
                Scheme = context.Request.Url.Scheme,
                Host = context.Request.Url.Host,
                Port = context.Request.Url.Port,
            });
#else
            //context.Response.Filter = cacheProvider.GetFilterStream(cachePath,
            //   context.Response.Filter,
            //   options);

            generator.Render(data, context.Response.Body, new RequestUrl() {
                Scheme = context.Request.Scheme,
                Host = context.Request.Host.Host,
                Port = (int)context.Request.Host.Port,
            });
#endif
        }

        private void HandleCache(string cachePath, HttpContext context, ICacheProvider cacheProvider)
        {
            var lastModifiedDate = cacheProvider.GetLastModifiedDateUtc(cachePath);
            context.Response.Headers.Add("Last-Modified", lastModifiedDate.ToString("r"));

#if SITEMAP_ASPNET
            cacheProvider.WriteCacheToStream(cachePath, context.Response.OutputStream);
#else

#endif

        }

        private ISitemapData GetSitemapData(string path, ICacheProvider cacheProvider)
        {
            ISitemapData result = DotNetSitemapConfig.Option.GetMapData(path);

            // Generate sitemap.xml if there is any SitemapIndex
            // If there is no route for path and requesting for sitemap.xml
            // Then get all SitemapIndex if there is any.

            if (result == null
                && path == DotNetSitemapConfig.Option.SitemapPath)
            {
                var indexLocs = DotNetSitemapConfig.Option.GetSitemapIndexPaths();
                if (indexLocs.Any())
                {
                    // If this is sitemap.xml
                    var siteMapIndex = new SitemapIndex();

                    foreach (var loc in indexLocs)
                    {
                        var siteMapItem = new Sitemap { Loc = loc };

                        if (cacheProvider.IsCached(loc))
                        {
                            siteMapItem.LastMod = cacheProvider.GetLastModifiedDateUtc(loc);
                        }

                        siteMapIndex.Sitemaps.Add(siteMapItem);

                    }

                    result = siteMapIndex;
                }

            }
            return result;
        }
    }
}
