using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Cache;
using System.Linq.Expressions;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapOption
    {
        public DotNetSitemapOption()
        {
            SitemapDataFunc = null;
            _cacheOption = null;
            _dataFunc = new Dictionary<string, Func<ISitemapData>>();
        }

        internal string _sitemapPath = "sitemap.xml";
        private static string _sitemapIndexPath = "sitemap-*.xml";

        internal Func<ISitemapData> SitemapDataFunc;
        internal SitemapCacheOption _cacheOption;
        Dictionary<string, Func<ISitemapData>> _dataFunc;
        public SitemapCacheOption Cache => _cacheOption;


        public void SetCache(SitemapCacheOption option)
        {
            _cacheOption = option;
        }


        public void SetDataFunc(Func<ISitemapData> sitemapDataFunc)
        {
            _dataFunc.Add("sitemap.xml", sitemapDataFunc);

        }
        public void SetDataFunc(string path, Func<ISitemapData> sitemapDataFunc)
        {
            if (!DataFuncPathFormatIsValid(path))
            {
                throw new Exception("Path is not valid. DataFunc Path must be a relative path. " +
                    "Path must not start with \\ or / or .");
            }

            if (!DataFuncPathRouteIsValid(path))
            {
                throw new Exception("Sitemap path must be formated as " +
                    $"{_sitemapPath} or {_sitemapIndexPath}");
            }
            _dataFunc.Add(path, sitemapDataFunc);


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
            return _cacheOption != null && _cacheOption.TimeOut != null;
        }

        public void SetSitemapPath(string path)
        {
            _sitemapPath = path;
        }
        public void SetSitemapIndexPath(string path)
        {
            _sitemapIndexPath = path;
        }
        public string GetSitemapPath()
        {
            return _sitemapPath;
        }
        public string GetSitemapIndexPath()
        {
            return _sitemapIndexPath;
        }
        public List<string> GetSitemapIndexLocs()
        {
            return _dataFunc.Where(p => p.Key != _sitemapPath).Select(p => p.Key).ToList();
        }

        #region helpers

        /// <summary>
        /// path do not start with '/' or '\' or '.'
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool DataFuncPathFormatIsValid(string path)
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
        /// <summary>
        /// Route is valid if it match with sitemap.xml or sitemap-*.xml format
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool DataFuncPathRouteIsValid(string path)
        {
            var siteMapRegex = _sitemapPath.Replace(".", @"\.").Replace("*", ".*");
            var siteMapIndexRegex = _sitemapIndexPath.Replace(".", @"\.").Replace("*", ".*");
            var match = Regex.Match(path, $@"({siteMapRegex}|{siteMapIndexRegex})");
            if (match.Success)
            {
                return true;
            }

            return false;
        }

        #endregion helpers
    }
}
