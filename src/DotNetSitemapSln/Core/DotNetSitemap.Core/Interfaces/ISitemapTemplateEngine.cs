using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Interfaces
{
    public interface ISitemapTemplateEngine
    {
        string Render(SitemapXml data, string templatePath);
    }
}
