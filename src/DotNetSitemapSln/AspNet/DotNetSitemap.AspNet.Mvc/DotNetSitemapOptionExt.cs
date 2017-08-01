using DotNetSitemap.Core;
using System;
using System.Web.Routing;
using DotNetSitemap.Core.Cache;
using System.Web;

namespace DotNetSitemap.AspNet
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

    }
}
