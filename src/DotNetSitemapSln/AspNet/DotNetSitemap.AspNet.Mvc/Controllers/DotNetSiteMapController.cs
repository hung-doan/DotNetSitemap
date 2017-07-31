//using DotNetSitemap.Core;
//using DotNetSitemap.Core.Models;
//using DotNetSitemap.NfMvc.Filters;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace DotNetSitemap.NfMvc.Controllers
//{
//    public class DotNetSiteMapController: Controller
//    {
//        [SiteMapCache]
//        public ActionResult Index()
//        {
            
//            var model = DotNetSitemapConfig.GetSiteMapData();
//            Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Xml;
//            return View("~/sitemap.cshtml", model);
//        }
//    }
//}
