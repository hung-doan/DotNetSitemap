using System;
using System.Collections.Generic;
using DotNetSitemap.Core.Constrains;
using DotNetSitemap.Core.Models.SitemapExtentions;

namespace DotNetSitemap.Core.Models.SingleSitemap
{
    public class Url
    {
        public string Loc;          //location
        public DateTimeOffset? LastMod;
        public ChangeFreq ChangeFreq;
        public double? Priority = 0.5;      // 0.0 to 1.0, default 0.5

        public List<ISitemapExtention> Extentions { get; set; }

        public Url()
        {
            Extentions = new List<ISitemapExtention>();
        }
    }
}
