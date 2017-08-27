using DotNetSitemap.Core.Middlewares;
using System;
using System.IO;

namespace DotNetSitemap.Core.Models.SitemapExtentions
{
    public interface ISitemapExtention
    {
        /// <summary>
        /// Prefix in xmlns:prefix="URI", Example : xmlns:example="http://www.example.com/schemas/example_schema">
        /// Prefix is example
        /// </summary>
        /// <returns>Prefix of Name Space</returns>
        string GetXmlNsPrefix();

        /// <summary>
        /// Uri in xmlns:prefix="URI", Example : xmlns:example="http://www.example.com/schemas/example_schema">
        /// Uri is http://www.example.com/schemas/example_schema
        /// </summary>
        /// <returns>Uri of Name Space</returns>
        string GetXmlNsUri();

        /// <summary>
        /// Render data to the Http response stream
        /// </summary>
        /// <param name="outputStream">Http Response output stream</param>
        /// <param name="requestUrl">HTTP Request uri</param>
        void Render(Stream outputStream, RequestUrl requestUrl);
    }
}
