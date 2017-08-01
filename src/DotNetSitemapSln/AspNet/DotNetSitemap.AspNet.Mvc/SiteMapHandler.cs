using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using System.IO;
using System.Web;
using System.Web.Routing;

namespace DotNetSitemap.AspNet
{
    public class SiteMapHandler : IHttpHandler, IRouteHandler
    {
        public bool IsReusable => true;
        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request.Url.AbsolutePath.Substring(1);
            var generator = DotNetSitemapConfig.Container.Resolve<ISiteMapGenerator>();
            var cacheProvider = DotNetSitemapConfig.Container.Resolve<ICacheProvider>();
            var data = DotNetSitemapConfig.Option.GetData(path);
            var cachePath = Path.Combine(DotNetSitemapConfig.Option.Cache.Location, path);
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
