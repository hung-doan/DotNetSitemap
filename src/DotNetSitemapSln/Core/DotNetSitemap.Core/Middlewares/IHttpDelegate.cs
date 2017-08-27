using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Middlewares
{
    public interface IHttpDelegate
    {
        void SetResponseHeader(string key, string value);
        void SetResponseStatusCode(int statusCode);
        void SetResponseContentType(string contentType);

    }
}
