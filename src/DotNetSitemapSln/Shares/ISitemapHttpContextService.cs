using System;
using System.Collections.Generic;
using System.Text;
#if SITEMAP_ASPNET
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif


namespace DotNetSitemap.Core.Services
{
    public interface ISitemapHttpContextService
    {
        void ProcessRequest(HttpContext context);
    }
}
