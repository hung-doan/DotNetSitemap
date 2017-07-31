using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core
{
    public interface ISiteMapGenerator
    {
        void Render(Stream outputStream, SitemapXml data);
        
    }
}
