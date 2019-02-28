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

namespace Amazonweb.Controllers.Template.Template1
{
    public class Template1Controller : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();

        [Route("u/{userName}/Index")]
        public ActionResult Index(string userName)
        {
            return View();
        }
        [Route("u/{userName}/GetProductASIN")]
        public JsonResult GetProductASIN(String UserId)
        {
            GetProductModel productSaveModel = new GetProductModel();
            productSaveModel.UserId = Guid.Parse(UserId);//Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetProduct, "POST", _request);
            //if (_response == null) return View();
            //----- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {

                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                //var jsonUniqueName = jsonData.unique_name;
                if (json.ContainsKey("unique_name"))
                {

                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                    _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["unique_name"].ToString());
                    if (_data.ContainsKey("Response"))
                    {
                        var ListProducts = JsonConvert.DeserializeObject<List<GetProductModel>>(_data["Response"].ToString());
                        var jSonValue = JsonConvert.SerializeObject(ListProducts, Formatting.Indented);
                        //  var jSonValue= json(strserialize, JsonRequestBehavior.AllowGet);
                        return Json(jSonValue, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json("data:Record Not Found");
            }
        }
        DataSet DsGetData(string signedurl)
        {
            try
            {
                //Create a request object using signed URL.
                WebRequest request = HttpWebRequest.Create(signedurl);
                //Get response in a stream
                Stream responseStream = request.GetResponse().GetResponseStream();

                DataSet DS = new DataSet();
                //Read returned resonpse stream into a dataset.
                //Note: You can also use XMLDocument element here to read response.
                DS.ReadXml(responseStream);
                responseStream.Close();

                return DS;
            }
            catch (Exception e)
            {
                //If there is an error capture it.
                //If you get an error, it could be either because of invalid keyword or you provided wrong access key.
                //lblError.Text = e.Message;
                System.Console.WriteLine("Caught Exception: " + e.Message);
                System.Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

            return null;
        }
        [Route("u/{userName}/JsonGetItemFromAmazon")]
        public JsonResult JsonGetItemFromAmazon(AmazonModel amazonModel)    //TODO: primitive parameters // Fixed
        {

            //if () { } else { }

            //Use SignedRequesthelper class to generate signed request. 
            SignedRequestHelper helper = new SignedRequestHelper();

            IDictionary<string, string> requestParams = new Dictionary<string, String>();
            requestParams["Service"] = "AWSECommerceService";
            String Operation = "ItemSearch";
            if (!String.IsNullOrWhiteSpace(amazonModel.ASIN))
            {
                Operation = "ItemLookup";
            }
            requestParams["Operation"] = Operation;
            requestParams["AWSAccessKeyId"] = "AKIAIB32UVMKXN37TKIA";
            requestParams["AssociateTag"] = "mobilea0477c9-20";

            #region If ASIN is Null 
            if (String.IsNullOrWhiteSpace(amazonModel.ASIN))
            {
                if (!string.IsNullOrWhiteSpace(amazonModel.CategoryNames))
                    requestParams["SearchIndex"] = amazonModel.CategoryNames;//"Baby";// HardCode
                if (!string.IsNullOrWhiteSpace(amazonModel.SearchItemName))
                    requestParams["Keywords"] = amazonModel.SearchItemName; //"Book"; // HardCode
                requestParams["sort"] = "price";
                // requestParams["BrowseNode"] = "search-alias=baby-products-intl-ship"; // HardCode
                if (!string.IsNullOrWhiteSpace(amazonModel.MinimumPrice))
                    requestParams["MinimumPrice"] = amazonModel.MinimumPrice;
                if (!string.IsNullOrWhiteSpace(amazonModel.MaximumPrice))
                    requestParams["MaximumPrice"] = amazonModel.MaximumPrice;
                requestParams["Brand"] = amazonModel.Brand;
                if (String.IsNullOrWhiteSpace(amazonModel.Brand))
                    requestParams["Title"] = amazonModel.Title;
            }
            #endregion
            else if (!String.IsNullOrWhiteSpace(amazonModel.ASIN))
            {
                requestParams["IdType"] = "ASIN";                   //TODO Dangerous Hard coded can be a checkbox in the web page
                requestParams["ItemId"] = amazonModel.ASIN;

            }
            requestParams["ResponseGroup"] = "Images,ItemAttributes,Offers,Reviews,SalesRank,";





            //Get signed URL in a variable
            string requestUrl = helper.Sign(requestParams);

            //Get response from signed request
            DataSet DS = DsGetData(requestUrl);
            if (DS != null)
            {
                //Serialize DataSet to make it suitable to send as json.
                var result = JsonConvert.SerializeObject(DS, Formatting.Indented);
                return Json(result, JsonRequestBehavior.AllowGet);
                //You can set debug point here and inspect content of Datased(DS).
                //it has few more tables that you might be interested in.
            }



            return Json("Not Found", JsonRequestBehavior.AllowGet);
            // Example of link https://webservices.amazon.com/onca/xml?AWSAccessKeyId=AKIAIB32UVMKXN37TKIA&AssociateTag=mobilea0477c9-20&IdType=ASIN&ItemId=B00X3Q0L3O&Operation=ItemLookup&ResponseGroup=Images%2CItemAttributes%2COffers&Service=AWSECommerceService&Timestamp=2018-07-04T09%3A02%3A14.000Z&Signature=D1RRwL8jdAcQ5XhrSdYImtJvK9FwE8DSaVlKfZ68t%2Fc%3D
        }


        [Route("u/{userName}/GetUserAbout")]
        public JsonResult GetUserAbout(String UserId)
        {
            UserAboutModel objUserAboutModel = new UserAboutModel();
            objUserAboutModel.UserId = Guid.Parse(UserId);//Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objUserAboutModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetUserAbout, "POST", _request);
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {
                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                //var jsonUniqueName = jsonData.unique_name;
                if (json.ContainsKey("unique_name"))
                {
                    ResponseModel ResModel = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                    // var jsonResponseCovt = JsonConvert.DeserializeObject<Dictionary<string, object>>(ResModel.Response);
                    var jwtToken = ResModel.Response;
                    dynamic ObjUserAbout = _JwtTokenManager.DecodeToken(jwtToken);
                    var jsonDeser = JsonConvert.DeserializeObject<Dictionary<string, object>>(ObjUserAbout);

                    if (jsonDeser.ContainsKey("unique_name"))
                    {
                        var listUserAbout = JsonConvert.DeserializeObject<UserAboutModel>(jsonDeser["unique_name"].ToString());

                        //var listUserAbout = JsonConvert.DeserializeObject<List<UserAboutModel>>(ObjUserAbout);
                        var jSonValue1 = JsonConvert.SerializeObject(listUserAbout, Formatting.Indented);
                        //  var jSonValue= json(strserialize, JsonRequestBehavior.AllowGet);
                        return Json(jSonValue1, JsonRequestBehavior.AllowGet);
                    }

                }

                return Json("data:Record Not Found");
            }
        }
        [Route("u/{userName}/About")]
        public ActionResult AboutIndex(string userName)
        {
            return View();
        }
     

        [Route("u/{userName}/Contactus")]

        public ActionResult Contactus(string UserName)
        {
            return View();
        }
        [Route("u/{userName}/SendMailContact")]
        public int SendMailContact(string receiverEmailId, string name, string message, string phone, string subject)
        {
            try
            {
                var senderEmail = new MailAddress("ashishsharma@accendos.in", name);
                var receiverEmail = new MailAddress(receiverEmailId, "Receiver");
                var password = "ashu89880";
                var body = "<b>Phone N0 is</b>= " + phone + "<p> Text Message= " + message + "</p>"+ "<p>Subject= "+ subject +"</p>";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,

                })
                {
                    smtp.Send(mess);
                }
                return 1;
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
                return 0;
                //ViewBag.Error = "Some Error";
            }
        }
        [Route("u/{userName}/services")]

        public ActionResult services()
        {
            return View();
        }
      
    }
}