using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core
{
    public class SiteMapGenerator : ISiteMapGenerator
    {

        private Encoding _encoding = new System.Text.UTF8Encoding();
        private Stream _outputStream;
        
        private void Write(string input)
        {
            var inputBytes = _encoding.GetBytes(input);
            _outputStream.Write(inputBytes, 0, inputBytes.Length);
        }
        private void RenderUrlset(UrlSet urlSet)
        {

            var xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            Write($"<urlset xmlns=\"{xmlns}\">");

            foreach (var url in urlSet.Urls)
            {
                Write($"<url>");

                Write($"<loc>{url.Loc}</loc>");
                if (url.LastMod != null) Write($"<lastmod>{url.LastMod:yyyy-MM-dd}</lastmod>");
                if (url.ChangeFreq != null) Write($"<changefreq>{url.ChangeFreq.ToString()}</changefreq>");
                if (url.Priority != null) Write($"<priority>{url.Priority}</priority>");

                Write($"</url>");
            }
            Write("</urlset>");
        }
        public void Render(Stream outputStream, SitemapXml data)
        {
            _outputStream = outputStream;
            var xmlVersion = "1.0";
            var xmlEncoding = "UTF-8";
            Write($"<?xml version=\"{xmlVersion}\" encoding=\"{xmlEncoding}\"?>");
            if (data.UrlSet != null)
            {
                RenderUrlset(data.UrlSet);
            }
        }
    }
}
