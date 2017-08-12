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
    public class SampleSitemapDataBuilder
    {
        public static ISitemapData GetAllSitemapData()
        {
            var result = new UrlSet();

            // Manual sitemap url's loc
            #region implement
            result.Urls.Add(new Url
            {
                Loc = "Product/Detail/1",
                ChangeFreq = ChangeFreq.Daily,
                LastMod = DateTimeOffset.Now.AddHours(1),
                Priority = 0.5
            });

            result.Urls.Add(new Url
            {
                Loc = "Product/Detail/2",
                ChangeFreq = ChangeFreq.Weekly,
                LastMod = DateTimeOffset.Now.AddHours(2),
                Priority = 0.6
            });
            #endregion implement

            // Use helper to gen url's loc
            #region implement
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
            #endregion implement

            // Use google image extention

            #region implement
            var urlWithExtention = new Url
            {
                Loc = helper.Action("List", "Product", new { id = 999 }),
                ChangeFreq = ChangeFreq.Weekly,
                LastMod = DateTimeOffset.Now.AddHours(2),
                Priority = 0.6
            };
            urlWithExtention.Extentions.Add(new GoogleImageExtention()
            {
                Images = new List<Image>()
                {
                    new Image{
                        Loc = "images/product-1.jpg",
                        Caption = "product 1 caption",
                        GeoLocation = "Limerick, Ireland 1",
                        Title = "product 1 title",
                        License= "A URL to the license of the image 1"
                    },
                    new Image{
                        Loc = "images/product-2.jpg",
                        Caption = "product 2 caption",
                        GeoLocation = "Limerick, Ireland 2",
                        Title = "product 2 title",
                        License= "A URL to the license of the image 2"
                    }
                }
            });
            result.Urls.Add(urlWithExtention);
            #endregion implement

            return result;
        }

        public static ISitemapData GetProductDetailSitemapData()
        {
            // Use can set url manually
            var result = new UrlSet();
            result.Urls.Add(new Url
            {
                Loc = "Product/Detail/1",
                ChangeFreq = ChangeFreq.Daily,
                LastMod = DateTimeOffset.Now.AddHours(1),
                Priority = 0.5
            });

            result.Urls.Add(new Url
            {
                Loc = "Product/Detail/2",
                ChangeFreq = ChangeFreq.Weekly,
                LastMod = DateTimeOffset.Now.AddHours(2),
                Priority = 0.6
            });

            result.Urls.Add(new Url
            {
                Loc = "Product/Detail/3",
                ChangeFreq = ChangeFreq.Monthly,
                LastMod = DateTimeOffset.Now.AddHours(3),
                Priority = 0.7
            });

            return result;
        }

        public static ISitemapData GetProductListSitemapData()
        {
            // Use can use UrlHelper to generate url
            var helper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var result = new UrlSet();
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
                Priority = 0.7
            });

            return result;
        }
    }
}