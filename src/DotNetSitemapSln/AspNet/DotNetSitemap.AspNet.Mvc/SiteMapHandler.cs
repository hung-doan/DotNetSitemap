using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace DotNetSitemap.AspNet.Mvc
{
    public class SiteMapHandler : IHttpHandler, IRouteHandler
    {
        public bool IsReusable => true;
        public void ProcessRequest(HttpContext context)
        {
            var generator = DotNetSitemapConfig.Container.Resolve<ISiteMapGenerator>();
            var cacheProvider = DotNetSitemapConfig.Container.Resolve<ICacheProvider>();
            var data = DotNetSitemapConfig.GetSiteMapData();
            var cachePath = Path.Combine(DotNetSitemapConfig.Option.Cache.Location, "sitemap.xml");
            context.Response.Filter = cacheProvider.GetFilterStream(cachePath,
                context.Response.Filter,
                DotNetSitemapConfig.Option);
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/xml";
            generator.Render(context.Response.OutputStream, data);

        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return this;
        }
    }
    //public class ResponseFilter : FileStream
    //{
    //    private Stream _rootFilter;
    //    public ResponseFilter(Stream rootFilter, string filePath)
    //        : base(filePath, FileMode.Create)
    //    {
    //        _rootFilter = rootFilter;

    //    }
    //    public override void Write(byte[] buffer, int offset, int count)
    //    {
    //        base.Write(buffer, offset, count);
    //        _rootFilter.Write(buffer, offset, count);

    //    }

    //    public override void Close()
    //    {
    //        base.Close();
    //        _rootFilter.Close();
    //    }
    //    public override void Flush()
    //    {
    //        base.Flush();
    //        _rootFilter.Flush();
    //    }

    //}
}
