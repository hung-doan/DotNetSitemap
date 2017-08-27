using DotNetSitemap.Core;
using System;
using System.Web.Routing;
using System.Web;
using System.Xml;
using System.Configuration;
using System.Web.Configuration;
using System.Linq;
using System.Collections.Generic;
using DotNetSitemap.Core.Middlewares.Caches;

namespace DotNetSitemap.AspNet
{
    public static class DotNetSitemapRegistration
    {
        public static void UseDotNetSiteMap(RouteCollection routes
            , Action<IDotNetSitemapOption> configureRoutes)
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
