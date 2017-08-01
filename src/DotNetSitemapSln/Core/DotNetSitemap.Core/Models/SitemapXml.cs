using System;
using System.Collections.Generic;
using System.Text;

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
