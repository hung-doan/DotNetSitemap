using DotNetSitemap.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Web.Mvc;
using DotNetSitemap.Core.Models;
using DotNetSitemap.Core.Cache;
using DotNetSitemap.AspNet.Mvc;
using System.Web;

namespace DotNetSitemap.NfMvc
{
    public static class DotNetSitemapOptionExt
    {
        static DotNetSitemapOptionExt()
        {
            DotNetSitemapConfig.Container.Register<ISiteMapGenerator, SiteMapGenerator>();
            DotNetSitemapConfig.Container.Register<ICacheProvider, LocalFileCacheProvider>();
        }
        public static void Register(this DotNetSitemapOption cfg, HttpServerUtility serverUtil, RouteCollection routes)
        {
            DotNetSitemapConfig.Option.SetCache(new SiteMapCacheOption
            {
                TimeOut = new TimeSpan(0, 1, 0),
                Location = serverUtil.MapPath("~/App_Data/cache_sitemap")
            });

            routes.Ignore("sitemap.xml");
            routes.Ignore("sitemap/*.xml");
        }
        public static void RegisterRoute(this DotNetSitemapOption cfg, RouteCollection routes)
        {

        }
        public static void RegisterRoute(this DotNetSitemapOption cfg, RouteCollection routes, string url)
        {
            routes.MapRoute(
                   name: "sitemapxml",
                   url: url,
                   defaults: new { controller = "DotNetSiteMap", action = "Index"}
               );
        }


    }
}
