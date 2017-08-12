using DotNetSitemap.Core.Models;
using DotNetSitemap.Core.Models.SitemapOptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core
{
    public interface ISitemapGenerator
    {
        void Render(ISitemapData data, Stream outputStream, RequestUrl requestUrl);
    }
}
