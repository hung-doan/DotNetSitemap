using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetSitemap.AspNet.MvcDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Detail(string id)
        {
            return View();
        }
        public ActionResult List(string id)
        {
            return View();
        }
    }
}