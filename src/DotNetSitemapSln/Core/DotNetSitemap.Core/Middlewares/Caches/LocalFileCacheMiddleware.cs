using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core.Middlewares.Caches
{
    public class LocalFileCacheMiddleware : SitemapMiddleware, ICacheProvider
    {
        public override void Invoke(MiddlewareContext context)
        {
            var options = DotNetSitemapConfig.Option;
            if (!options.Cacheable)
            {
                Next.Invoke(context);
                return;
            }
            
            // If current request is cachable 
            // and It was cached
            // and It is not expired
            // then we return the cached version

            var cachePath = Path.Combine(options.CacheLocation
                    , context.RequestUrl.DataPath);

            if (IsCached(cachePath)
                && !IsExpired(cachePath, options))
            {
                HandleCache(cachePath, context);
                return; // Do not need to handle response
            }

            ///
            /// HANDLE response
            ///
            var originalStream = context.OutputStream;

            using (var fs = new FileStream(cachePath, FileMode.Create))
            {
                context.OutputStream = fs;

                //Before response
                Next.Invoke(context);
                //After response

                context.OutputStream = originalStream;
                fs.Position = 0;
                fs.CopyTo(context.OutputStream);
            }

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
        public void HandleCache(string cachePath, MiddlewareContext context)
        {
            var lastModifiedDate = GetLastModifiedDateUtc(cachePath);
            context.HttpDelegate.SetResponseHeader("Last-Modified", lastModifiedDate.ToString("r"));

            using (var fs = new FileStream(cachePath, FileMode.Open))
            {
                fs.CopyTo(context.OutputStream);
            }

        }

    }
}
