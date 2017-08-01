using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.AspNet
{
    public class LocalFileCacheProvider : ICacheProvider
    {
        public Stream GetFilterStream(string filePath, Stream inputStream, DotNetSitemapOption options)
        {
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            return new LocalFileCacheStreamFilter(inputStream, filePath, options);
        }

        public bool IsCached(string filePath)
        {
            return File.Exists(filePath);
        }

        public bool IsExpired(string filePath, DotNetSitemapOption options)
        {
            var lastWrite = GetLastModifiedDate(filePath);

            return lastWrite.Add(options.Cache.TimeOut).CompareTo(DateTime.Now) < 0;
        }
        public DateTime GetLastModifiedDate(string filePath)
        {
            return File.GetLastWriteTime(filePath);
        }
        public void WriteCacheToStream(string filePath, Stream outputStream)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                fs.CopyTo(outputStream);
            }
        }
    }
    public class LocalFileCacheStreamFilter : FileStream
    {
        private Stream _rootFilter;
        private DotNetSitemapOption _options;
        public LocalFileCacheStreamFilter(Stream rootFilter
            , string filePath
            , DotNetSitemapOption options)
            : base(filePath, FileMode.Create)
        {
            _rootFilter = rootFilter;
            _options = options;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            base.Write(buffer, offset, count);
            _rootFilter.Write(buffer, offset, count);

        }

        public override void Flush()
        {
            base.Flush();
            _rootFilter.Flush();
        }
    }

}
