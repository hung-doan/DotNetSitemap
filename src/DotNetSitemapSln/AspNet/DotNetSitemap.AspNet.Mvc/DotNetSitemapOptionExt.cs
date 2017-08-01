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
    public static class DotNetSitemapOptionExt
    {
        private static string _sitemapIndexPath = "sitemap/*.xml";
        static DotNetSitemapOptionExt()
        {
            DotNetSitemapConfig.Container.Register<ISiteMapGenerator, SiteMapGenerator>();
            DotNetSitemapConfig.Container.Register<ICacheProvider, LocalFileCacheProvider>();
        }
        public static void SetSitemapIndexPath(string path)
        {
            _sitemapIndexPath = path;
        }
        public static void Register(this DotNetSitemapOption cfg, HttpServerUtility serverUtil, RouteCollection routes)
        {
            DotNetSitemapConfig.Option.SetCache(new SiteMapCacheOption
            {
                TimeOut = new TimeSpan(0, 1, 0),
                Location = serverUtil.MapPath("~/App_Data/cache_sitemap")
            });
            ;
            routes.Ignore(_sitemapPath);
            routes.Ignore(_sitemapIndexPath);
        }

    }
}
