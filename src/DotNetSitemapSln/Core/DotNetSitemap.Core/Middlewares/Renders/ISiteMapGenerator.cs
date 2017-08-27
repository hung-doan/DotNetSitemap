using DotNetSitemap.Core.Models;
using DotNetSitemap.Core.Middlewares;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core.Middlewares.Renders
{
    public interface ISitemapGenerator
    {
        void Render(ISitemapData data, Stream outputStream, RequestUrl requestUrl);
    }
}
