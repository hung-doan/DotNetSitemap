using DotNetSitemap.Core;
using DotNetSitemap.Core.Cache;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.AspNet
{
    // TODO: Move to core
    public class LocalFileCacheProvider : ICacheProvider
    {
        public Stream GetFilterStream(string filePath, Stream inputStream, IDotNetSitemapOption options)
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

        public bool IsExpired(string filePath, IDotNetSitemapOption options)
        {
            var lastWrite = GetLastModifiedDateUtc(filePath);

            return lastWrite.Add(options.CacheTimeOut.Value).CompareTo(DateTimeOffset.UtcNow) < 0;
        }
        public DateTimeOffset GetLastModifiedDateUtc(string filePath)
        {
            return File.GetLastWriteTimeUtc(filePath);
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
        private IDotNetSitemapOption _options;
        public LocalFileCacheStreamFilter(Stream rootFilter
            , string filePath
            , IDotNetSitemapOption options)
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
