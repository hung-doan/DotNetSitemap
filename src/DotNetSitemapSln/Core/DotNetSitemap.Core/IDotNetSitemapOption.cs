using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;

namespace DotNetSitemap.Core
{
    public interface IDotNetSitemapOption
    {
        string SitemapPath { get; set; }
        string SitemapIndexPath { get; set; }

        string CacheLocation { get; set; }
        TimeSpan? CacheTimeOut { get; set; }
        bool Cacheable { get; }

        
        Dictionary<string, Func<ISitemapData>> DataMaps { get; }
        IDotNetSitemapOption MapData(string path, Func<ISitemapData> sitemapDataFunc);
        IEnumerable<string> GetSitemapIndexPaths();
        ISitemapData GetMapData(string path);
    }
}