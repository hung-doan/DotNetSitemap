using DotNetSitemap.AspNet;
using DotNetSitemap.Core;

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
using DotNetSitemap.Sample;
namespace DotNetSitemap.AspNet.MvcDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DotNetSitemapRegistration.UseDotNetSiteMap(RouteTable.Routes, options => {
                //options.MapData("sitemap.xml",...); // Mean you are using <UrlSet>
                //options.MapData("sitemap-[your custom].xml",...); // Mean you are using <Sitemapindex>
                
                options.MapData("sitemap.xml", SampleSitemapDataBuilder.GetDataWithManualUrl);
                options.MapData("sitemap-with-manual-url.xml", SampleSitemapDataBuilder.GetDataWithManualUrl);
                options.MapData("sitemap-with-auto-url.xml", SampleSitemapDataBuilder.GetDataWithUrlGeneratedByMvcAction);
                options.MapData("sitemap-with-google-image-extention.xml", SampleSitemapDataBuilder.GetDataWithGoogleImageExtention);

                //Setting cache
                options.CacheTimeOut = new TimeSpan(0, 1, 0);
                options.CacheLocation = Server.MapPath("~/App_Data/cache_sitemap");
            });

            


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
