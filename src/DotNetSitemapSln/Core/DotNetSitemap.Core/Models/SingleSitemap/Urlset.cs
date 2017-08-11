using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetSitemap.Core.Models.SingleSitemap
{
    public partial class UrlSet
    {
        public List<Url> Urls;

        public UrlSet()
        {
            Urls = new List<Url>();
        }

    }
}
