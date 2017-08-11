using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Helpers;

namespace DotNetSitemap.Core.Models.MultipleSitemap
{
    public partial class SitemapIndex
    {
        public SitemapIndex()
        {
            Sitemaps = new List<Sitemap>();
        }
        public List<Sitemap> Sitemaps { get; set; }
    }
}
