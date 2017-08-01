using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Models
{
    public class SitemapIndex
    {
        public List<Sitemap> Sitemaps { get; set; }

        public SitemapIndex()
        {
            Sitemaps = new List<Sitemap>();
        }
    }
}
