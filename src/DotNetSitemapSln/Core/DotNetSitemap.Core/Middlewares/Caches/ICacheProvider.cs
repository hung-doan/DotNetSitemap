using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core.Middlewares.Caches
{
    public interface ICacheProvider
    {
        bool IsCached(string cacheKey);
        DateTimeOffset GetLastModifiedDateUtc(string cacheKey);
        bool IsExpired(string cacheKey, IDotNetSitemapOption options);
    }
}
