using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryWebApplication.Controllers
{
    public class APODController : Controller
    {
        // GET: APOD
        public ActionResult Index(string date)
        {
            // Get request to NASA
            var url = "https://api.nasa.gov/planetary/apod";

            var client = new WebClient();

            byte[] body = null;
            string result = null;
            dynamic obj = null;

            if (date == null || date == "") { date = DateTime.Now.Date.ToString(); }

            if (date != null && date != "")
            {
                //+"&date="+date
                body = client.DownloadData(url + "?api_key=DEMO_KEY");
                result = System.Text.Encoding.UTF8.GetString(body);
                obj = JObject.Parse(result);

                ViewData["example"] = result;
            }

            if (obj != null)
            {
                ViewData["date"] = obj["date"];
                ViewData["url"] = obj["url"];
                ViewData["title"] = obj["title"];
                ViewData["explanation"] = obj["explanation"];
            }
            else
            {
                ViewData["explanation"] = "В момента данните не са достъпни.";
            }

            return View();
        }
    }
}