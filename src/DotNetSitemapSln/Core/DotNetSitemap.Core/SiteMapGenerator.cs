using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetSitemap.Core.Models.MultipleSitemap;
using DotNetSitemap.Core.Models.SingleSitemap;
using DotNetSitemap.Core.Models.SitemapOptions;

namespace DotNetSitemap.Core
{
    public class SitemapGenerator : ISitemapGenerator
    {
        private string _xmlVersion = "1.0";
        private string _xmlEncoding = "UTF-8";
        private Encoding _encoding = new UTF8Encoding();
        private Stream _outputStream;

        public void Render(ISitemapData data, Stream outputStream, RequestUrl requestUrl)
        {
            _outputStream = outputStream;
            Write($"<?xml version=\"{_xmlVersion}\" encoding=\"{_xmlEncoding}\"?>");
            data.Render(outputStream, requestUrl);
        }

        private void Write(string input)
        {
            var inputBytes = _encoding.GetBytes(input);
            _outputStream.Write(inputBytes, 0, inputBytes.Length);
        }

    }
}
