using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetSitemap.Core.Models.SingleSitemap
{
    public class UrlSet : ISitemapData
    {
        public List<Url> Urls;

        public UrlSet()
        {
            Urls = new List<Url>();
        }

        public void Render(Stream outputStream, Uri requestUri)
        {
            throw new NotImplementedException();
        }
    }
}
