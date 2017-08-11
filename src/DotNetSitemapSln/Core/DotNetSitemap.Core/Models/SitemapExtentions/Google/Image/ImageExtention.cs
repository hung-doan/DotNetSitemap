using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetSitemap.Core.Models.SitemapExtentions.Google.Image
{
    public class ImageExtention : ISitemapExtention
    {
        private string _xmlnsUri = "http://www.google.com/schemas/sitemap-image/1.1";
        private string _xmlnsPrefix = "image";

        public List<Image> Images { get; set; }

        public string GetXmlNsUri()
        {
            return _xmlnsUri;
        }
        public string GetXmlNsPrefix()
        {
            return _xmlnsPrefix;
        }

        public void Render(Stream outputStream, Uri requestUri)
        {
            throw new NotImplementedException();
        }

    }
}
