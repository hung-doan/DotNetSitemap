using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Models.MultipleSitemap;
using DotNetSitemap.Core.Models.SingleSitemap;

namespace DotNetSitemap.Core
{
    public class SiteMapGenerator : ISiteMapGenerator
    {
        private string _xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private Encoding _encoding = new System.Text.UTF8Encoding();
        private Stream _outputStream;
        private UriBuilder _baseUriBuilder;
        public void Render(Stream outputStream, SitemapXml data, Uri requestUri)
        {
            _outputStream = outputStream;
            var xmlVersion = "1.0";
            var xmlEncoding = "UTF-8";
            _baseUriBuilder = new UriBuilder(requestUri.Scheme, requestUri.Host, requestUri.Port);
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
        public string GetUrl(string loc)
        {
            _baseUriBuilder.Path = loc;
            return _baseUriBuilder.ToString();
        }
        private void RenderUrlset(UrlSet urlSet)
        {


            Write($"<urlset xmlns=\"{_xmlns}\">");

            foreach (var url in urlSet.Urls)
            {
                if(url.Loc.IndexOf("://") < 0)
                {
                    url.Loc = GetUrl(url.Loc);
                }
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
                if (sitemap.Loc.IndexOf("://") < 0)
                {
                    sitemap.Loc = GetUrl(sitemap.Loc);
                }

                Write($"<sitemap>");

                Write($"<loc>{sitemap.Loc}</loc>");

                if (sitemap.LastMod != null) Write($"<lastmod>{sitemap.LastMod.Value.ToUniversalTime().ToString("O")}</lastmod>");


                Write($"</sitemap>");
            }

            Write("</sitemapindex>");
        }


    }
}
