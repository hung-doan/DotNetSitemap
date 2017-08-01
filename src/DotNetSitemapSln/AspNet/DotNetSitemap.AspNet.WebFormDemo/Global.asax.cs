using DotNetSitemap.Core;
using DotNetSitemap.AspNet;
using DotNetSitemap.Core.Constrains;
using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DotNetSitemap.AspNet.WebFormDemo
{
    public class Global : HttpApplication
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

        void Application_Start(object sender, EventArgs e)
        {
            DotNetSitemapConfig.Option.Register(Server, RouteTable.Routes);
            DotNetSitemapConfig.Option.SetSitemapDataFunc(SiteMapData);

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}