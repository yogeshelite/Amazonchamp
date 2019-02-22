using Amazonweb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.Controllers
{
    public class DefaultController : Controller
    {
     
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
       
    }
}