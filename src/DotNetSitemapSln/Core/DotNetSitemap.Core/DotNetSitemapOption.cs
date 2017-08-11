using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Cache;
using System.Linq.Expressions;
using System.Linq;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapOption
    {
        internal string _sitemapPath = "sitemap.xml";
        internal Func<ISitemapData> SitemapDataFunc;
        internal SiteMapCacheOption _cacheOption;
        Dictionary<string, Func<ISitemapData>> _dataFunc;
        public DotNetSitemapOption()
        {
            SitemapDataFunc = null;
            _cacheOption = null;
            _dataFunc = new Dictionary<string, Func<ISitemapData>>();
        }
        public SiteMapCacheOption Cache => _cacheOption;
        public void SetCache(SiteMapCacheOption option)
        {
            _cacheOption = option;
        }


        public void SetDataFunc(Func<ISitemapData> sitemapDataFunc)
        {
            _dataFunc.Add("sitemap.xml", sitemapDataFunc);

        }
        public void SetDataFunc(string path, Func<ISitemapData> sitemapDataFunc)
        {
            if (!DataFuncPathIsValid(path))
            {
                throw new Exception("Path is not valid. DataFunc Path must be a relative path. " +
                    "Path must not start with \\ or / or .");
            }
            _dataFunc.Add(path, sitemapDataFunc);


        }

        /// <summary>
        /// path do not start with '/' or '\' or '.'
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool DataFuncPathIsValid(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (path[0] == '/'
                || path[0] == '\\'
                || path[0] == '.')
            {
                return false;
            }

            return true;
        }
        public ISitemapData GetData(string path)
        {
            if (!_dataFunc.ContainsKey(path))
            {
                return null;
            }
            return _dataFunc[path]();
        }

        public bool IsCacheable()
        {
            return _cacheOption != null;
        }

        public void SetSitemapPath(string path)
        {
            _sitemapPath = path;
        }
        public string GetSitemapPath()
        {
            return _sitemapPath;
        }
        public List<string> GetSitemapIndexLocs()
        {
            return _dataFunc.Where(p => p.Key != _sitemapPath).Select(p => p.Key).ToList();
        }
    }
}
