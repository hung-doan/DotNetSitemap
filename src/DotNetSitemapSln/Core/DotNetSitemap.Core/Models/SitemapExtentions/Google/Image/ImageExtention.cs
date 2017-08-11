using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetSitemap.Core.Models.SitemapExtentions.Google.Image
{
    public class ImageExtention : ISitemapExtention
    {
        private string _xmlns = "http://www.google.com/schemas/sitemap-image/1.1";
        public string GetXmlNs()
        {
            return _xmlns;
        }

        public List<Image> Images { get; set; }
        public void Render(Stream outputStream, Uri requestUri)
        {
            throw new NotImplementedException();
        }
    }
}
