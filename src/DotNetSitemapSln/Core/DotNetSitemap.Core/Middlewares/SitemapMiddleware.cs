using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSitemap.Core.Middlewares
{
    public abstract class SitemapMiddleware : ISitemapMiddleware
    {
        public ISitemapMiddleware Next { get; set; }


        public abstract void Invoke(MiddlewareContext context);
    }
}
