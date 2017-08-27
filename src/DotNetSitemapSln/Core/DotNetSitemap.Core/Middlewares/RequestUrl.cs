using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Middlewares
{
    public class RequestUrl
    {
        // https://sample.com/sitemap.xml
        public string Path;     // "/sitemap.xml"
        public string DataPath;     // "sitemap.xml"
        public string Scheme;   // "https"
        public string Host;     // "sample.com"
        public int Port;        // 80
    }
}
