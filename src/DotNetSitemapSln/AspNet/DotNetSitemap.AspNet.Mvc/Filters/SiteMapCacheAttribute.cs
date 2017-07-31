//using DotNetSitemap.Core;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace DotNetSitemap.NfMvc.Filters
//{

//    public class SiteMapCacheAttribute : FilterAttribute, IActionFilter
//    {
//        string _fileName = "sitemap.xml";
//        string _contenType = "application/xml";


//        private bool IsExpired()
//        {
//            return DateTime.Compare(GetLastCachedDate().Add(DotNetSitemapConfig.Option.GetTimeOut().Value), DateTime.Now) < 0;
//        }
//        private bool IsCached()
//        {
//            return File.Exists(DotNetSitemapConfig.GetCacheFilePath(_fileName));
//        }


//        private DateTime GetLastCachedDate()
//        {
//            return File.GetLastWriteTime(DotNetSitemapConfig.GetCacheFilePath(_fileName));
//        }


//        public void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            var cachable = DotNetSitemapConfig.IsCachable();
//            filterContext.HttpContext.Items.Add("cachable", cachable);

//            if (!IsCached() || cachable)
//            {
//                // If expired or there is no cached file then do nothing
//                if (IsExpired())
//                {
//                    return;
//                }

//                // If It is not expired, then return cached file
//                filterContext.HttpContext.Response.ContentType = _contenType;
//                filterContext.HttpContext.Response.TransmitFile(DotNetSitemapConfig.GetCacheFilePath(_fileName));
//                filterContext.HttpContext.Response.Flush();
//                filterContext.HttpContext.Response.Close();
//                filterContext.Result = new EmptyResult();
//            }

//        }

//        public void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            var cachable = (bool)filterContext.HttpContext.Items["cachable"];
//            if (cachable)
//            {
//                filterContext.RequestContext.HttpContext.Response.Filter = new SaveCacheFilterStream(
//                    filterContext.RequestContext.HttpContext.Response.Filter,
//                    DotNetSitemapConfig.GetCacheFilePath(_fileName), FileMode.Create);

//            }
//        }

//    }

//    public class SaveCacheFilterStream : FileStream
//    {
//        private Stream _rootFilter;
//        public SaveCacheFilterStream(Stream rootFilter, string filePath, FileMode access)
//            : base(filePath, access)
//        {
//            _rootFilter = rootFilter;
//        }
//        public override void Write(byte[] buffer, int offset, int count)
//        {
//            base.Write(buffer, offset, count);
//            _rootFilter.Write(buffer, offset, count);

//        }

//        public override void Close()
//        {
//            base.Close();
//            _rootFilter.Close();
//        }
//        public override void Flush()
//        {
//            base.Flush();
//            _rootFilter.Flush();
//        }

//    }
//}
