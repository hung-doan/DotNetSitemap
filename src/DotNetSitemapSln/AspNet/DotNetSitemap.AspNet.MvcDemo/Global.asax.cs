using DotNetSitemap.AspNet;
using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Constrains;
using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DotNetSitemap.Core.Models.SingleSitemap;

namespace DotNetSitemap.NfMvc.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private SitemapXml SiteMapData()
        {
            var data = new SitemapXml();
            data.UrlSet = new UrlSet();
            data.UrlSet.Urls = new List<Url> {
                new Url{
                    Loc = "http://abc.com",
                    ChangeFreq = ChangeFreq.Weekly,
                    LastMod = DateTime.Today,
                    Priority = 0.2
                }
            };
            return data;
        }
        protected void Application_Start()
        {
            DotNetSitemapConfig.Option.Register(Server, RouteTable.Routes);
            DotNetSitemapConfig.Option.Cache.TimeOut = new TimeSpan(0, 1, 0);
            DotNetSitemapConfig.Option.SetDataFunc("sitemap/product.xml",SiteMapData);
            DotNetSitemapConfig.Option.SetDataFunc("sitemap/category.xml", SiteMapData);


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
