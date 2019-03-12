using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amazonweb.Models;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using AmzonWebApi.Models;
using System.Net;
using System.Net.Mail;
using Amazonweb.TemplateThemes.TemplateData;

namespace Amazonweb.Controllers.Templates.Template2
{
    public class Template2Controller : Controller
    {
        // GET: Template2 
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        TemplateData TemplateData = new TemplateData();

        //[Route("u/{userName/index}")]
        public ActionResult Index(string userName)
        { 

            return View();
        }



        [Route("u/{userName}/GetProductAsin")]
        public JsonResult GetProductASIN(String UserId)
        {

            var jsn = TemplateData.GetProductASIN(UserId);
            return Json(jsn);

        }

        //[Route("u/{userName}/AmazonItem")]
        //public JsonResult JsonGetItemFromAmazon(AmazonModel amazonModel)    //TODO: primitive parameters // Fixed
        //{

        //    var jsOn = TemplateData.JsonGetItemFromAmazon(amazonModel);
        //    return Json(jsOn);
        //}



        //[Route("u/{userName}/About")]
        //public ActionResult About(string userName)
        //{
        //    return View();
        //}
        [Route("~/u/{userName}/Contactus")]

        public ActionResult Contactus(string UserName)
        {
            return View();
        }
        //[Route("u/{userName}/Mail")]
        //public int SendMailContact(string receiverEmailId, string name, string message, string phone, string subject)
        //{
        //    var MailData = TemplateData.SendMailContact(receiverEmailId, name, message, phone, subject);
        //    return 1;

        //}
        //[Route("u/{userName}/services")]

        //public ActionResult services()
        //{
        //    return View();
        //}
        //[Route("u/{userName}/Product")]

        //public ActionResult Product()
        //{
        //    return View();
        //}
    }
}