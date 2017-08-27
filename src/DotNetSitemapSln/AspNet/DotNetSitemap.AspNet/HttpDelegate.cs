using DotNetSitemap.Core.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DotNetSitemap.AspNet
{
    public class HttpDelegate : IHttpDelegate
    {
        HttpContext _context;
        public HttpDelegate(HttpContext context)
        {
            _context = context;
        }

        public void SetResponseContentType(string contentType)
        {
            _context.Response.ContentType = contentType;
        }

        public void SetResponseHeader(string key, string value)
        {
            _context.Response.AddHeader(key, value);
        }

        public void SetResponseStatusCode(int statusCode)
        {
            _context.Response.StatusCode = statusCode;
        }
    }
}
