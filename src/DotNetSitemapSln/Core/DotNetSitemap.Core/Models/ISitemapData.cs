using DotNetSitemap.Core.Middlewares;
using System.IO;

namespace DotNetSitemap.Core.Models
{
    public interface ISitemapData
    {
        void Render(Stream outputStream, RequestUrl requestUrl);
    }
}
