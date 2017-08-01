using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Cache;
using System.Linq.Expressions;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapOption
    {
        internal Func<SitemapXml> SitemapDataFunc;
        internal SiteMapCacheOption _cacheOption;
        Dictionary<string, Func<SitemapXml>> _dataFunc;
        public DotNetSitemapOption()
        {
            SitemapDataFunc = null;
            _cacheOption = null;
            _dataFunc = new Dictionary<string, Func<SitemapXml>>();
        }
        public SiteMapCacheOption Cache => _cacheOption;
        public void SetCache(SiteMapCacheOption option)
        {
            _cacheOption = option;
        }

        
        public void SetDataFunc(string path, Func<SitemapXml> sitemapDataFunc)
        {
            _dataFunc.Add(path, sitemapDataFunc);

        }

      

        public SitemapXml GetData(string path)
        {
            return _dataFunc[path]();
        }

        public bool IsCacheable()
        {
            return _cacheOption != null;
        }
    }
}
