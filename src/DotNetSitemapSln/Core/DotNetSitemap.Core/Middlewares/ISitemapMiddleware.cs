using System.Collections.Specialized;

namespace DotNetSitemap.Core.Middlewares
{
    public interface ISitemapMiddleware
    {
        void Invoke(MiddlewareContext context);
        ISitemapMiddleware Next { get; set; }
    }
}