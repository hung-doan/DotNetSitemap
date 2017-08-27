using DotNetSitemap.Core.Middlewares;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSitemap.Core.Middlewares
{
    public class MiddlewareContext
    {
        public MiddlewareContext()
        {
            Items = new Dictionary<string, object>();
        }
        public Stream OutputStream;
        public RequestUrl RequestUrl;
        public IHttpDelegate HttpDelegate;
        public Dictionary<string,object> Items { get; set; }
    }
}
