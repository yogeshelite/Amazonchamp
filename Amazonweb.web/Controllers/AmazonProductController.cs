using Amazonweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.Controllers
{
    public class AmazonProductController : Controller
    {
        // GET: AmazonProduct
        public ActionResult Index()
        {
            AmazonProductModel AmzModel = new AmazonProductModel();
            return View(AmzModel);
        }
    }
}