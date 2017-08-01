using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core
{
    public class SiteMapGenerator : ISiteMapGenerator
    {
        private string _xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private Encoding _encoding = new System.Text.UTF8Encoding();
        private Stream _outputStream;
        public void Render(Stream outputStream, SitemapXml data)
        {
            _outputStream = outputStream;
            var xmlVersion = "1.0";
            var xmlEncoding = "UTF-8";
            Write($"<?xml version=\"{xmlVersion}\" encoding=\"{xmlEncoding}\"?>");
            if (data == null)
            {
                return;
            }
            if (data.UrlSet.Urls.Count > 0)
            {
                RenderUrlset(data.UrlSet);
            }
            if (data.SitemapIndex.Sitemaps.Count > 0)
            {
                RenderSitemapIndex(data.SitemapIndex);
            }
        }


        private void Write(string input)
        {
            var inputBytes = _encoding.GetBytes(input);
            _outputStream.Write(inputBytes, 0, inputBytes.Length);
        }

        private void RenderUrlset(UrlSet urlSet)
        {


            Write($"<urlset xmlns=\"{_xmlns}\">");

            foreach (var url in urlSet.Urls)
            {
                Write($"<url>");

                Write($"<loc>{url.Loc}</loc>");
                if (url.LastMod != null) Write($"<lastmod>{url.LastMod.Value.ToUniversalTime().ToString("O")}</lastmod>");
                if (url.ChangeFreq != null) Write($"<changefreq>{url.ChangeFreq.ToString()}</changefreq>");
                if (url.Priority != null) Write($"<priority>{url.Priority}</priority>");

                Write($"</url>");
            }
            Write("</urlset>");
        }

        private void RenderSitemapIndex(SitemapIndex sitemapIndex)
        {
            Write($"<sitemapindex xmlns=\"{_xmlns}\">");

            foreach (var sitemap in sitemapIndex.Sitemaps)
            {
                Write($"<sitemap>");

                Write($"<loc>{sitemap.Loc}</loc>");

                if (sitemap.LastMod != null) Write($"<lastmod>{sitemap.LastMod.Value.ToUniversalTime().ToString("O")}</lastmod>");


                Write($"</sitemap>");
            }

            Write("</sitemapindex>");
        }


    }
}
