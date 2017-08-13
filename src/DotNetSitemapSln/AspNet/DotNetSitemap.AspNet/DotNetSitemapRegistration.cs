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
using DotNetSitemap.Core.Services;

namespace DotNetSitemap.AspNet
{
    public static class DotNetSitemapRegistration
    {

        static DotNetSitemapRegistration()
        {
            DotNetSitemapConfig.Container.Register<ISitemapHttpContextService, SitemapHttpContextService>();
            DotNetSitemapConfig.Container.Register<ICacheProvider, LocalFileCacheProvider>();
        }
        public static void UseDotNetSiteMap(RouteCollection routes
            , Action<IDotNetSitemapOption> configureRoutes = null)
        {
            var option = DotNetSitemapConfig.Container.Resolve<IDotNetSitemapOption>();
            if(configureRoutes != null)
            {
                configureRoutes.Invoke(option);
            }
            
            DotNetSitemapConfig.SetOption(option);

            routes.Ignore(option.SitemapPath);
            routes.Ignore("{*sitemapIndexUrl}", new
            {
                sitemapIndexUrl = option.SitemapIndexPath.Replace(".", @"\.").Replace("*", ".*")
            });

        }

    }
}
