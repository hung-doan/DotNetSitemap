using DotNetSitemap.Core.Middlewares.Caches;
using DotNetSitemap.Core.Middlewares.Renders;
using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapConfig
    {

        static DotNetSitemapConfig()
        {
            //Use default container
            _container = new DotNetSitemapContainer();

            _container.Register<ISitemapGenerator, SitemapGenerator>();
            _container.Register<IDotNetSitemapOption, DotNetSitemapOption>();
            _container.Register<ICacheProvider, LocalFileCacheMiddleware>();

        }

        private static IDotNetSitemapOption _option;
        private static IDotNetSitemapContainer _container;

        public static IDotNetSitemapOption Option => _option;
        public static IDotNetSitemapContainer Container => _container;

        public static void SetOption(IDotNetSitemapOption option)
        {
            _option = option;
        }
        public static void SetContainer(IDotNetSitemapContainer container)
        {
            _container = container;
        }

    }
}
