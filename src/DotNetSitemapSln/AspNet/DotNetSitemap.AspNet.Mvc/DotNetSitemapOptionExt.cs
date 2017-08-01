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
        private static string _sitemapName = "netsitemap";
        private static string _sitemapIndexName = "netsitemap-index";
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
            ;

            
            string sitemapPath = null;
            string sitemapIndexPath = null;
            var hdlrs = ConfigurationManager.GetSection("system.webServer");
            //foreach (HttpHandlerAction handler in hdlrs.Handlers)
            //{
            //    if (sitemapPath != null && sitemapIndexPath != null)
            //    {
            //        break;
            //    }
            //    foreach (PropertyInformation prop in handler.ElementInformation.Properties)
            //    {
            //        if (prop.Value.ToString() == "sitemap.xml")
            //        {
            //            sitemapPath = handler.Path;
            //        }
            //        if (prop.Name == "name" && (string)prop.Value == _sitemapIndexName)
            //        {
            //            sitemapIndexPath = handler.Path;
            //        }
            //    }
            //}

            if (sitemapPath == null)
            {
                routes.Ignore("sitemap.xml");
            }
            else
            {
                routes.Ignore(sitemapPath);
            }
            if (sitemapIndexPath == null)
            {
                routes.Ignore("*/sitemap.xml");
            }
            else
            {
                routes.Ignore(sitemapIndexPath);
            }
        }

    }
}
