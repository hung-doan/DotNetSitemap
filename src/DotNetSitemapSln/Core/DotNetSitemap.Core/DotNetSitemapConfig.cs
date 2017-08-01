using DotNetSitemap.Core.Cache;
using DotNetSitemap.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapConfig
    {
        private static readonly DotNetSitemapOption _option;
        private static IDotNetSitemapContainer _container;
        
        static DotNetSitemapConfig()
        {
            _option = new DotNetSitemapOption();
            _container = new DotNetSitemapContainer();
            
        }
        
        public static DotNetSitemapOption Option => _option;
        public static IDotNetSitemapContainer Container => _container;
        /// <summary>
        /// Check if able to cache sitemap pages,
        /// Sitemap is cachable if : Cache dir & cache timeout were initialized.
        /// </summary>
        /// <returns></returns>
        public static bool IsCachable()
        {
            return Option._cacheOption != null;
        }
        public static void SetContainer(IDotNetSitemapContainer container)
        {
            _container = container;
        }

        
    }
}
