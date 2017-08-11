using DotNetSitemap.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


namespace DotNetSitemap.Core.Models.SingleSitemap
{
    public partial class UrlSet : ISitemapData
    {
        private readonly Encoding _encoding = new UTF8Encoding();
        private Stream _outputStream;
        private string _xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        public void Render(Stream outputStream, Uri requestUri)
        {
            // Save output stream to reuse
            _outputStream = outputStream;

            // Getting XML Namespace of Extentions
            var extentionXmlNs = Urls.SelectMany(p => p.Extentions.Select(ext
                => $"xmlns:{ext.GetXmlNsPrefix()}=\"{ext.GetXmlNsUri()}\"")
                ).Distinct().ToList();

            string extentionXmlNsAttr = "";
            if (extentionXmlNs.Any())
            {
                extentionXmlNsAttr = " " + string.Join(" ", extentionXmlNs);
            }

            Write($"<urlset xmlns=\"{_xmlns}\"{extentionXmlNsAttr}>");
            foreach (var url in Urls)
            {
                if (!UriHelpers.IsAbsoluteUrl(url.Loc))
                {
                    url.Loc = UriHelpers.BuildUrl(requestUri, url.Loc);
                }
                Write($"<url>");

                Write($"<loc>{url.Loc}</loc>");
                if (url.LastMod != null) Write($"<lastmod>{url.LastMod.Value.ToUniversalTime().ToString("O")}</lastmod>");
                if (url.ChangeFreq != null) Write($"<changefreq>{url.ChangeFreq.ToString()}</changefreq>");
                if (url.Priority != null) Write($"<priority>{url.Priority}</priority>");
                
                //Start to render extentions
                foreach(var extention in url.Extentions)
                {
                    extention.Render(outputStream, requestUri);
                }

                Write($"</url>");
            }
            Write("</urlset>");
        }
        private void Write(string input)
        {
            var inputBytes = _encoding.GetBytes(input);
            _outputStream.Write(inputBytes, 0, inputBytes.Length);
        }
    }
}
