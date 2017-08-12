using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Routing;
using System.Linq;
using System.Net;
using DotNetSitemap.Core.Models.MultipleSitemap;
using DotNetSitemap.Core.Services;

namespace DotNetSitemap.AspNet
{
    public class SitemapHandler : IHttpHandler, IRouteHandler
    {
        public bool IsReusable => true;

        /// <summary>
        /// This function to use MvcRoute in case of not using HttpHandlers
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }

        public void ProcessRequest(HttpContext context)
        {
            var sitemapService = DotNetSitemapConfig.Container.Resolve<ISitemapHttpContextService>();
            sitemapService.ProcessRequest(context);
        }
        
    }
}
