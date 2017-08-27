using DotNetSitemap.Core.Middlewares;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.AspNetCore
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
            _context.Response.Headers.Add(key,value);
        }

        public void SetResponseStatusCode(int statusCode)
        {
            _context.Response.StatusCode = statusCode;
        }
    }
}
