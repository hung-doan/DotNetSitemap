using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNetSitemap.Core
{
    public partial class DotNetSitemapOption : IDotNetSitemapOption
    {
        public DotNetSitemapOption()
        {
            _dataMaps = new Dictionary<string, Func<ISitemapData>>();
            SitemapPath = "sitemap.xml";
            SitemapIndexPath = "sitemap-*.xml";
        }
        Dictionary<string, Func<ISitemapData>> _dataMaps;

        public string CacheLocation { get; set; }
        public TimeSpan? CacheTimeOut { get; set; }
        public string SitemapPath { get; set; }
        public string SitemapIndexPath { get; set; }
        public Dictionary<string, Func<ISitemapData>> DataMaps => _dataMaps;
        public bool Cacheable => CacheTimeOut != null && CacheLocation != null;
        public IDotNetSitemapOption MapData(string path, Func<ISitemapData> sitemapDataFunc)
        {
            if (!DataFuncPathFormatIsValid(path))
            {
                throw new Exception("Path is not valid. DataFunc Path must be a relative path. " +
                    "Path must not start with \\ or / or .");
            }

            if (!DataFuncPathRouteIsValid(path))
            {
                throw new Exception("Sitemap path must be formated as " +
                    $"{SitemapPath} or {SitemapIndexPath}");
            }
            _dataMaps.Add(path, sitemapDataFunc);

            return this;
        }
        public IEnumerable<string> GetSitemapIndexPaths()
        {
            return _dataMaps.Where(p => p.Key != SitemapPath).Select(p => p.Key);
        }
        public ISitemapData GetMapData(string path)
        {
            if (!_dataMaps.ContainsKey(path))
            {
                return null;
            }
            return _dataMaps[path]();
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
            var siteMapRegex = SitemapPath.Replace(".", @"\.").Replace("*", ".*");
            var siteMapIndexRegex = SitemapIndexPath.Replace(".", @"\.").Replace("*", ".*");
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
