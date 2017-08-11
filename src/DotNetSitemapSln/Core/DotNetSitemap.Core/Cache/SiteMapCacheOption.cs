using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Cache
{
    public class SitemapCacheOption
    {
        public TimeSpan? TimeOut { get; set; }
        public string Location { get; set; }
    }
}
