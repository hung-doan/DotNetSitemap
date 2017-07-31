using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Cache;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapOption
    {
        internal Func<SitemapXml> SitemapDataFunc;
        internal SiteMapCacheOption _cacheOption;
        public DotNetSitemapOption()
        {
            SitemapDataFunc = null;
            _cacheOption = null;
        }
        public SiteMapCacheOption Cache => _cacheOption;
        public void SetCache(SiteMapCacheOption option)
        {
            _cacheOption = option;
        }
        public void SetSitemapDataFunc(Func<SitemapXml> sitemapDataFunc)
        {
            SitemapDataFunc = sitemapDataFunc;
        }
       
    }
}
