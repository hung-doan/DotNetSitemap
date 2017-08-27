using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSitemap.Core.Middlewares
{
    public class SitemapMiddlewareHandler
    {
        static ISitemapMiddleware _firstMiddleware;
        static ISitemapMiddleware _lastMiddleware;

        public static void Add<T>(T middleware) where T : ISitemapMiddleware
        {
            if (_lastMiddleware == null)
            {
                _firstMiddleware = middleware;
                _lastMiddleware = _firstMiddleware;
            }
            else
            {
                _lastMiddleware.Next = middleware;
                _lastMiddleware = middleware;
            }
        }
        public static void Add<T>() where T : ISitemapMiddleware
        {
            ISitemapMiddleware instance = Activator.CreateInstance(typeof(T)) as ISitemapMiddleware;
            if (_lastMiddleware == null)
            {
                _firstMiddleware = instance;
                _lastMiddleware = _firstMiddleware;
            }
            else
            {
                _lastMiddleware.Next = instance;
                _lastMiddleware = instance;
            }
        }

        public static void Invoke(MiddlewareContext context)
        {
            // If there is no middleware, then do nothing
            if(_firstMiddleware == null)
            {
                return;
            }
            _firstMiddleware.Invoke(context);
        }
    }
}
