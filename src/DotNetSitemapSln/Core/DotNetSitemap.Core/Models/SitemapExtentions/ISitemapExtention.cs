using System;
using System.IO;

namespace DotNetSitemap.Core.Models.SitemapExtentions
{
    public interface ISitemapExtention
    {
        string GetXmlNs();
        void Render(Stream outputStream, Uri requestUri);
    }
}
