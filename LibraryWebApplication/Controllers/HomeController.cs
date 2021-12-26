using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["url"] = "https://www.advancedsciencenews.com/wp-content/uploads/2020/01/brett-ritchie-1vKTnwLMdqs-unsplash1.jpg";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}