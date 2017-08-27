//using DotNetSitemap.Core.Constrains;
//using DotNetSitemap.Core.Models;
//using DotNetSitemap.Core.Models.SingleSitemap;
//using DotNetSitemap.Core.Models.SitemapExtentions.Google.Image;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace DotNetSitemap.Sample
//{
//    public partial class SampleSitemapDataBuilder
//    {
//        public static ISitemapData GetDataWithManualUrl()
//        {
//            var result = new UrlSet();

//            result.Urls.Add(new Url
//            {
//                Loc = "Product/Detail/1",
//                ChangeFreq = ChangeFreq.Daily,
//                LastMod = DateTimeOffset.Now.AddHours(1),
//                Priority = 0.5
//            });

//            result.Urls.Add(new Url
//            {
//                Loc = "Product/Detail/2",
//                ChangeFreq = ChangeFreq.Weekly,
//                LastMod = DateTimeOffset.Now.AddHours(2),
//                Priority = 0.6
//            });

//            result.Urls.Add(new Url
//            {
//                Loc = "Product/Detail/3",
//                ChangeFreq = ChangeFreq.Monthly,
//                LastMod = DateTimeOffset.Now.AddHours(3),
//                Priority = 0.7
//            });

//            result.Urls.Add(new Url
//            {
//                Loc = "Product/Detail/4",
//                ChangeFreq = ChangeFreq.Yearly,
//                LastMod = DateTimeOffset.Now.AddHours(4),
//                Priority = 0.8
//            });

//            result.Urls.Add(new Url
//            {
//                Loc = "Product/Detail/5",
//                ChangeFreq = ChangeFreq.Never,
//                LastMod = DateTimeOffset.Now.AddHours(5),
//                Priority = 0.9
//            });

//            return result;
//        }

//        public static ISitemapData GetDataWithGoogleImageExtention()
//        {
//            var result = new UrlSet();

//            // Normal url without Extention
//            result.Urls.Add(new Url
//            {
//                Loc = "Product/Detail/1",
//                ChangeFreq = ChangeFreq.Daily,
//                LastMod = DateTimeOffset.Now.AddHours(1),
//                Priority = 0.5
//            });

//            // Create a base Url
//            var urlWithExtention = new Url
//            {
//                Loc = "Product/Detail/2",
//                ChangeFreq = ChangeFreq.Weekly,
//                LastMod = DateTimeOffset.Now.AddHours(2),
//                Priority = 0.6
//            };

//            // Then add Extentions to url you want to
//            // For example: add 2 images into 
//            urlWithExtention.Extentions.Add(new GoogleImageExtention()
//            {
//                Images = new List<Image>()
//                {
//                    new Image{
//                        Loc = "images/product-1.jpg",
//                        Caption = "product 1 caption",
//                        GeoLocation = "Limerick, Ireland 1",
//                        Title = "product 1 title",
//                        License= "A URL to the license of the image 1"
//                    },
//                    new Image{
//                        Loc = "images/product-2.jpg",
//                        Caption = "product 2 caption",
//                        GeoLocation = "Limerick, Ireland 2",
//                        Title = "product 2 title",
//                        License= "A URL to the license of the image 2"
//                    }
//                }
//            });

//            result.Urls.Add(urlWithExtention);

//            return result;
//        }

//    }
//}