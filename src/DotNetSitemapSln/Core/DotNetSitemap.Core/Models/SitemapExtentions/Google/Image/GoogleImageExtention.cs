using DotNetSitemap.Core.Helpers;
using DotNetSitemap.Core.Middlewares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core.Models.SitemapExtentions.Google.Image
{
    public class GoogleImageExtention : ISitemapExtention
    {
        private string _xmlnsUri = "http://www.google.com/schemas/sitemap-image/1.1";
        private string _xmlnsPrefix = "image";
        private readonly Encoding _encoding = new UTF8Encoding();
        private Stream _outputStream;

        public List<Image> Images { get; set; }

        public string GetXmlNsUri()
        {
            return _xmlnsUri;
        }
        public string GetXmlNsPrefix()
        {
            return _xmlnsPrefix;
        }

        public void Render(Stream outputStream, RequestUrl requestUrl)
        {
            _outputStream = outputStream;

            foreach (var img in Images)
            {
                if (!UriHelpers.IsAbsoluteUrl(img.Loc))
                {
                    img.Loc = UriHelpers.BuildUrl(requestUrl, img.Loc);
                }

                Write($"<image:image>");

                Write($"<image:loc>{img.Loc}</image:loc>");

                if (img.Caption != null) Write($"<image:caption>{img.Caption}</image:caption>");
                if (img.GeoLocation != null) Write($"<image:geo_location>{img.GeoLocation}</image:geo_location>");
                if (img.Title != null) Write($"<image:title>{img.Title}</image:title>");
                if (img.License != null) Write($"<image:license>{img.License}</image:license>");

                Write($"</image:image>");
            }

        }

        private void Write(string input)
        {
            var inputBytes = _encoding.GetBytes(input);
            _outputStream.Write(inputBytes, 0, inputBytes.Length);

        }

    }
}
