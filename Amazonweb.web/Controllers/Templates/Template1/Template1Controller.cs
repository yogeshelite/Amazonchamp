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
using Amazonweb.web.Handlers;
using System.Web.Routing;



namespace Amazonweb.Controllers.Template.Template1
{
    public class Template1Controller : Controller
    { HttpContextBase httpContext;
        HttpRequest HttpRequest;
        RequestContext requestContext;




        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        TemplateData TemplateData = new TemplateData();

        //[Route("u/{userName/index}")]
        public ActionResult Index(string userName)
        { 
            //var cookiesValue = Services.GetCookie(httpContext, "Template");
            //var json = HttpRequest.Cookies["Template"].Values.ToString();
            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //var result = serializer.Deserialize<Dictionary<string, object>>(json);

            // return result;
            return View();
        }

       
        [Route("u/{userName}/GetProductAsin")]
        public JsonResult GetProductASIN(String UserId ,string controller)
        {
            var jsn = TemplateData.GetProductASIN(UserId);
            return Json(jsn);
        }
        [Route("u/{userName}/JsonGetItemFromAmazon")]
        public JsonResult JsonGetItemFromAmazon(AmazonModel amazonModel)    //TODO: primitive parameters // Fixed
        {

            var jsOn = TemplateData.JsonGetItemFromAmazon(amazonModel);
            return Json(jsOn);
        }


        [Route("u/{userName}/GetUserAbout")]
        public JsonResult GetUserAbout(String UserId)
        {
            var jsn = TemplateData.GetUserAbout(UserId);
            return Json(jsn);
            
        }
        [Route("u/{userName}/About")]
        public ActionResult About(string userName)
        {
            return View();
        }


        [Route("u/{userName}/Contactus")]

        public ActionResult Contactus(string UserName)
        {
            return View();
        }
        [Route("u/{userName}/SendMail")]
        public int SendMailContact(string receiverEmailId, string name, string message, string phone, string subject)
        {
            var MailData = TemplateData.SendMailContact(receiverEmailId, name, message, phone, subject);
            return 1;
            //try
            //{
            //    var senderEmail = new MailAddress("ashishsharma@accendos.in", name);
            //    var receiverEmail = new MailAddress(receiverEmailId, "Receiver");
            //    var password = "ashu89880";
            //    var body = "<b>Phone N0 is</b>= " + phone + "<p> Text Message= " + message + "</p>" + "<p>Subject= " + subject + "</p>";
            //    var smtp = new SmtpClient
            //    {
            //        Host = "smtp.gmail.com",
            //        Port = 587,
            //        EnableSsl = true,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        Credentials = new NetworkCredential(senderEmail.Address, password)
            //    };
            //    using (var mess = new MailMessage(senderEmail, receiverEmail)
            //    {
            //        Subject = subject,
            //        Body = body,
            //        IsBodyHtml = true,

            //    })
            //    {
            //        smtp.Send(mess);
            //    }
            //    return 1;
            //}
            //catch (Exception ex)
            //{
            //    string exception = ex.ToString();
            //    return 0;
            //    //ViewBag.Error = "Some Error";
            //}
        }
        [Route("u/{userName}/services")]

        public ActionResult services()
        {
            return View();
        }
        [Route("u/{userName}/Product")]

        public ActionResult Product(String UserId)
        {
            return View();
        }

    }
}