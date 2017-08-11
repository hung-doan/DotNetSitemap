using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DotNetSitemap.Core.Models.SitemapExtentions.Google.Image
{
    
    public class Image
    {
        public string Loc { get; set; }
        public string Caption { get; set; }
        public string GeoLocation { get; set; }
        public string Title { get; set; }
        public string License { get; set; }
    }
}
