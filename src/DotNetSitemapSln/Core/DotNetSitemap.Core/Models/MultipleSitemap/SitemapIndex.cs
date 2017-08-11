using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Helpers;

namespace DotNetSitemap.Core.Models.MultipleSitemap
{
    public class SitemapIndex : ISitemapData
    {
        public SitemapIndex()
        {
            Sitemaps = new List<Sitemap>();
        }
        public List<Sitemap> Sitemaps { get; set; }



        private readonly Encoding _encoding = new UTF8Encoding();
        private Stream _outputStream;
        private string _xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        public void Render(Stream outputStream, Uri requestUri)
        {
            _outputStream = outputStream;
            Write($"<sitemapindex xmlns=\"{_xmlns}\">");

            foreach (var sitemap in Sitemaps)
            {
                if (sitemap.Loc.IndexOf("://", StringComparison.Ordinal) < 0)
                {
                    sitemap.Loc = UriHelpers.BuildUrl(requestUri, sitemap.Loc);
                }

                Write($"<sitemap>");

                Write($"<loc>{sitemap.Loc}</loc>");

                if (sitemap.LastMod != null) Write($"<lastmod>{sitemap.LastMod.Value.ToUniversalTime().ToString("O")}</lastmod>");


                Write($"</sitemap>");
            }

            Write("</sitemapindex>");
        }
        private void Write(string input)
        {
            var inputBytes = _encoding.GetBytes(input);
            _outputStream.Write(inputBytes, 0, inputBytes.Length);
        }
    }
}
