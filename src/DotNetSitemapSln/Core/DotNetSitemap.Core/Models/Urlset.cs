using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Models
{
    public class UrlSet
    {
        public List<Url> Urls;

        public UrlSet()
        {
            Urls = new List<Url>();
        }
    }
}
