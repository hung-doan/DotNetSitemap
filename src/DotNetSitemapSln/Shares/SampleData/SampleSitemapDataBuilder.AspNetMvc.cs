using DotNetSitemap.Core.Constrains;
using DotNetSitemap.Core.Models;
using DotNetSitemap.Core.Models.SingleSitemap;
using DotNetSitemap.Core.Models.SitemapExtentions.Google.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetSitemap.Sample
{
    public partial class SampleSitemapDataBuilder
    {
        public static ISitemapData GetDataWithUrlGeneratedByMvcAction()
        {
            var result = new UrlSet();

            // Use helper to gen url's loc
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            result.Urls.Add(new Url
            {
                Loc = helper.Action("List", "Product", new { id = 1 }),
                ChangeFreq = ChangeFreq.Daily,
                LastMod = DateTimeOffset.Now.AddHours(1),
                Priority = 0.5
            });

            result.Urls.Add(new Url
            {
                Loc = helper.Action("List", "Product", new { id = 2 }),
                ChangeFreq = ChangeFreq.Weekly,
                LastMod = DateTimeOffset.Now.AddHours(2),
                Priority = 0.6
            });
			
			result.Urls.Add(new Url
            {
                Loc = helper.Action("List", "Product", new { id = 3 }),
                ChangeFreq = ChangeFreq.Monthly,
                LastMod = DateTimeOffset.Now.AddHours(3),
                Priority = 0.6
            });

            return result;
        }

    }
}