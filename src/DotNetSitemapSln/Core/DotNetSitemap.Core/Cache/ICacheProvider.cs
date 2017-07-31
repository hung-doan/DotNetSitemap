using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core.Cache
{
    public interface ICacheProvider
    {
        Stream GetFilterStream(string cacheKey, Stream inputStream, DotNetSitemapOption options);
    }
}
