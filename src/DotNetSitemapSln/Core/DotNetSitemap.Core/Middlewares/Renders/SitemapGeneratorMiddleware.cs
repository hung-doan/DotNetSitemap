using DotNetSitemap.Core.Middlewares.Caches;
using DotNetSitemap.Core.Models;
using DotNetSitemap.Core.Models.MultipleSitemap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DotNetSitemap.Core.Middlewares.Renders
{
    public class SitemapGeneratorMiddleware : SitemapMiddleware
    {
        public override void Invoke(MiddlewareContext context)
        {
            // Then render the page with data
            var generator = DotNetSitemapConfig.Container.Resolve<ISitemapGenerator>();
            var data = GetSitemapData(context);
            if (data == null)
            {
                context.HttpDelegate.SetResponseStatusCode(404);
                return;
            }
            generator.Render(data, context.OutputStream, context.RequestUrl);
        }

        public ISitemapData GetSitemapData(MiddlewareContext context)
        {
            var cacheProvider = DotNetSitemapConfig.Container.Resolve<ICacheProvider>();
            var result = DotNetSitemapConfig.Option.GetMapData(context.RequestUrl.DataPath);

            // Generate sitemap.xml if there is any SitemapIndex
            // If there is no route for path and requesting for sitemap.xml
            // Then get all SitemapIndex if there is any.

            if (result == null
                && context.RequestUrl.DataPath == DotNetSitemapConfig.Option.SitemapPath)
            {
                var indexLocs = DotNetSitemapConfig.Option.GetSitemapIndexPaths();
                if (indexLocs.Any())
                {
                    // If this is sitemap.xml
                    var siteMapIndex = new SitemapIndex();

                    foreach (var loc in indexLocs)
                    {
                        var siteMapItem = new Sitemap { Loc = loc };

                        if (cacheProvider != null && cacheProvider.IsCached(loc))
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
