using DotNetSitemap.Core;
using System;
using System.Web.Routing;
using DotNetSitemap.Core.Cache;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Web.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace DotNetSitemap.AspNet
{
    public static class DotNetSitemapOptionExtentions
    {
        
        static DotNetSitemapOptionExtentions()
        {
            DotNetSitemapConfig.Container.Register<ISitemapGenerator, SitemapGenerator>();
            DotNetSitemapConfig.Container.Register<ICacheProvider, LocalFileCacheProvider>();
        }
        
        public static void Register(this DotNetSitemapOption cfg, HttpServerUtility serverUtil, RouteCollection routes)
        {
            DotNetSitemapConfig.Option.SetCache(new SitemapCacheOption
            {
                TimeOut = null,
                Location = serverUtil.MapPath("~/App_Data/cache_sitemap")
            });
            ;
            routes.Ignore(cfg.GetSitemapPath());
            routes.Ignore("{*sitemapIndexUrl}", new { sitemapIndexUrl = cfg.GetSitemapIndexPath().Replace(".", @"\.").Replace("*", ".*") });
            
        }

    }
}
