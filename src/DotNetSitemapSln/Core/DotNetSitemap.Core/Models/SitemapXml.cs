using System;
using System.Collections.Generic;
using System.Text;
using DotNetSitemap.Core.Models.MultipleSitemap;
using DotNetSitemap.Core.Models.SingleSitemap;

namespace DotNetSitemap.Core.Models
{
    public class SitemapXml
    {
        public UrlSet UrlSet;
        public SitemapIndex SitemapIndex;
        public SitemapXml()
        {
            UrlSet = new UrlSet();
            SitemapIndex = new SitemapIndex();
        }
    }
}
