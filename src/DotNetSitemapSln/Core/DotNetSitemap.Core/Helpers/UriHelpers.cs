using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Helpers
{
    public class UriHelpers
    {
        public static  string BuildUrl(Uri baseUri, string path)
        {
            var builder = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port)
            {
                Path = path
            };
            return builder.ToString();

        }
    }
}
