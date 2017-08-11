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

namespace DotNetSitemap.AspNet.MvcDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DotNetSitemapConfig.Option.Register(Server, RouteTable.Routes);

            DotNetSitemapConfig.Option.SetDataFunc("sitemap.xml", SitemapDataBuilder.GetAllSitemapData);

            //DotNetSitemapConfig.Option.SetDataFunc("sitemap-product-detail.xml", SitemapDataBuilder.GetProductDetailSitemapData);
            //DotNetSitemapConfig.Option.SetDataFunc("sitemap-product-list.xml", SitemapDataBuilder.GetProductListSitemapData);

            ////Setting up cache
            //DotNetSitemapConfig.Option.Cache.TimeOut = new TimeSpan(0, 1, 0);
            //DotNetSitemapConfig.Option.Cache.Location = Server.MapPath("~/App_Data/cache_sitemap");




            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
