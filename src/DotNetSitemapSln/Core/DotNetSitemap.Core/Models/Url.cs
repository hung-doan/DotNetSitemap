using DotNetSitemap.Core.Constrains;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core.Models
{
    public class Url
    {
        public string Loc;          //location
        public DateTimeOffset? LastMod;
        public ChangeFreq ChangeFreq;
        public double? Priority = 0.5;      // 0.0 to 1.0, default 0.5
    }
}
