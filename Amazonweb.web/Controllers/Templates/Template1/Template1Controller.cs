using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.Controllers.Template.Template1
{
    public class Template1Controller : Controller
    {
        // GET: Template1
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contactus()
        {
            return View();
        }
    }
}