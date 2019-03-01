using Amazonweb.Models;
using AmzonWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Amazonweb.web.Controllers
{

    public class HomeController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        List<SubscriptionPlanModel> ListPlan;
        [Route("Dashboard")]
        public ActionResult Index()
        {
            UserModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            if(MdUser==null) return RedirectToAction("Login");
            ViewBag.UserName = MdUser.UserName;
            return View(MdUser);
        }



        public ActionResult Logout()
        {
            Services.RemoveCookie(this.ControllerContext.HttpContext, "usr");
            return RedirectToAction("Login");
        }
        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult RegisterUser(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(userModel));


                    //----------Call Api
                    var _response = Services.GetApiResponse(Constant.ApiRegister, "POST", _request);
                    if (_response == null) return null;
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        if (json.ContainsKey("unique_name"))
                        {
                            var login = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                            ViewBag.RegisterMessage = login.Response;

                            if (!login.Success)
                            {

                                return View("Registeration", userModel);
                            }
                            //var jsonString = "{\"Id\":\"" + login.Id + "\",\"UserName\":\"" + login.Response + "\"}";
                            //Services.SetCookie(this.ControllerContext.HttpContext, "usr", _JwtTokenManager.GenerateToken(jsonString.ToString()));
                           SendMail.SendMailContact(userModel.Email, "Registraion Mail ", userModel.UserName, userModel.Password);
                            //return RedirectToAction("Index");

                        }
                    }

                }
                catch (Exception ex)
                {

                }


            }

            return View("Registeration", userModel);
        }

        [HttpPost]
        [Route("AutheticateUser")]

        public ActionResult AutheticateUser(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));


                    //----------Call Api
                    var _response = Services.GetApiResponse(Constant.ApiLogin, "POST", _request);
                    if (_response == null) return View(loginModel);
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        if (json.ContainsKey("unique_name"))
                        {
                            ResponseModel login = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                            if (!login.Success) { ViewBag.Message = login.Response; return View("Login", loginModel); }
                            //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                            String VarResponse = login.Response;
                            string[] ArrResponse = VarResponse.Split(',');
                            var jsonString = "{\"Id\":\"" + login.Id + "\",\"UserName\":\"" + ArrResponse[0] + "\",\"Email\":\""+ ArrResponse[1] +"\"}";
                            Services.SetCookie(this.ControllerContext.HttpContext, "usr", _JwtTokenManager.GenerateToken(jsonString.ToString()));

                            //Services.SetCookie(this.ControllerContext.HttpContext, "usr", _JwtTokenManager.GenerateToken(login.Id.Value.ToString()));
                            // Services.SetCookie(this.ControllerContext.HttpContext, "usrName", login.Response.ToString());
                            return RedirectToAction("Index");

                        }

                    }

                }
                catch (Exception ex)
                {
                    return View("Login", loginModel);

                }

            }
            // return RedirectToAction("Index");
            return View("Login", loginModel);

        }


        [Route("Registeration")]
        public ActionResult Registeration()
        {
            return View();
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult GetAmazonProduct(AmazonModel amazonModel)
        {
            //AmazonModel amazonModel = new AmazonModel();
            //var json= GetItemFromAmazon(amazonModel);
            var ProductCategoryList = GetListCategory();
            ViewBag.ProductCategoryList = new SelectList(ProductCategoryList, "Id", "Category");

            return View();
        }
        public ActionResult CheckScore(AmazonModel objModel)
        {
            return View();
        }
        public List<CateoryModel> GetListCategory()
        {
            var ProductCategoryList = new List<CateoryModel>();
            var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));

            var _response = Services.GetApiResponse(Constant.ApiCategory, "POST", _request);
            //  if (_response == null) return View();
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
                    ProductCategoryList = JsonConvert.DeserializeObject<List<CateoryModel>>(json["unique_name"].ToString());
                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json

                }

            }
            return ProductCategoryList;

        }
        public string GetItemFromAmazon(AmazonModel amazonModel)    //TODO: primitive parameters // Fixed
        {

            //if () { } else { }

            //Use SignedRequesthelper class to generate signed request. 
            SignedRequestHelper helper = new SignedRequestHelper();

            IDictionary<string, string> requestParams = new Dictionary<string, String>();
            requestParams["Service"] = "AWSECommerceService";

            requestParams["AssociateTag"] = "mobilea0477c9-20";
            requestParams["Operation"] = "ItemLookup";
            requestParams["IdType"] = "ASIN";                   //TODO Dangerous Hard coded can be a checkbox in the web page
            requestParams["ItemId"] = amazonModel.ASIN;
            requestParams["ResponseGroup"] = "Images,ItemAttributes,Offers,Reviews";

            //Get signed URL in a variable
            string requestUrl = helper.Sign(requestParams);

            //Get response from signed request
            var DS = GetData(requestUrl);
            //var result ="";
            //if (DS != null)
            //{

            //    result = JsonConvert.SerializeObject(DS, Formatting.Indented);
            //   return DS;
            //    //return Json(result, JsonRequestBehavior.AllowGet);
            //    //You can set debug point here and inspect content of Datased(DS).
            //    //it has few more tables that you might be interested in.
            //}


            return DS;
            // return Json("Not Found", JsonRequestBehavior.AllowGet);
            // Example of link https://webservices.amazon.com/onca/xml?AWSAccessKeyId=AKIAIB32UVMKXN37TKIA&AssociateTag=mobilea0477c9-20&IdType=ASIN&ItemId=B00X3Q0L3O&Operation=ItemLookup&ResponseGroup=Images%2CItemAttributes%2COffers&Service=AWSECommerceService&Timestamp=2018-07-04T09%3A02%3A14.000Z&Signature=D1RRwL8jdAcQ5XhrSdYImtJvK9FwE8DSaVlKfZ68t%2Fc%3D
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
        string GetData(string signedurl)
        {
            try
            {
                //Create a request object using signed URL.
                WebRequest request = HttpWebRequest.Create(signedurl);
                //Get response in a stream
                Stream responseStream = request.GetResponse().GetResponseStream();
                using (var read = new StreamReader(responseStream))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(read.ReadToEnd());
                    return JsonConvert.SerializeXmlNode(doc);
                }

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SearchProduct(FormCollection collection)
        {

            String ASIN = collection["ASIN"].ToString();
            AmazonModel amazonModel = new AmazonModel();
            amazonModel.ASIN = ASIN;
            var ds = Json(GetItemFromAmazon(amazonModel)).Data;
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(ds.ToString());


            if (data.ContainsKey("ItemLookupResponse"))
            {
                var ItemLookupResponseStrig = data.ContainsKey("ItemLookupResponse");
                //var ItemLookupResponse = JsonConvert.DeserializeObject<Dictionary<string, object> >data.ContainsKey("ItemLookupResponse").ToString());

                amazonModel = JsonConvert.DeserializeObject<AmazonModel>(data["ItemLookupResponse"].ToString());

            }

            // long categoryId = long.Parse(collection["categoryId"].ToString());

            //ProductSaveModel productSaveModel = new ProductSaveModel()
            //{
            //    ASIN = collection["ASIN"].ToString(),
            //    categoryId = long.Parse(collection["categoryId"].ToString()),
            //    UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            //};


            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel objChangePassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objChangePassword.Id = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;


                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objChangePassword));


                    //----------Call Api
                    var _response = Services.GetApiResponse(Constant.ApiChangePassword, "POST", _request);
                    if (_response == null) return null;
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        if (json.ContainsKey("unique_name"))
                        {

                            Services.RemoveCookie(this.ControllerContext.HttpContext, "usr");
                            return RedirectToAction("Login");

                        }
                    }

                }
                catch (Exception ex)
                {

                }


            }

            return View("ChangePassword", objChangePassword);
        }
        //public int SendMailContact(string receiverEmailId, string subject, string userName, string userPassword)
        //{
        //    try
        //    {
        //        var senderEmail = new MailAddress("puneet@cityinfomart.com", userName);
        //        var receiverEmail = new MailAddress(receiverEmailId, "Receiver");
        //        var password = "puneet123#";
        //        var body = "<b>Thanks For Registration</b><p> User Name Is=" + userName + "</p><p> Password Is= " + userPassword + "</p>";
        //        var smtp = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = false,
        //            Credentials = new NetworkCredential(senderEmail.Address, password)
        //        };
        //        using (var mess = new MailMessage(senderEmail, receiverEmail)
        //        {
        //            Subject = subject,
        //            Body = body,
        //            IsBodyHtml = true,

        //        })
        //        {
        //            smtp.Send(mess);
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        string exception = ex.ToString();
        //        return 0;
        //        //ViewBag.Error = "Some Error";
        //    }
        //}

        [HttpPost]
        public JsonResult ForGetPassword(LoginModel loginModel)
        {
            String UserEmailId = "";
            String ReturnJson = "";
            if (!String.IsNullOrWhiteSpace(loginModel.UserName))
            {
                UserEmailId = loginModel.UserName;
                try
                {

                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));


                    //----------Call Api
                    var _response = Services.GetApiResponse(Constant.ApiForgetPassword, "POST", _request);
                    if (_response == null) return null;
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        if (json.ContainsKey("unique_name"))
                        {
                            ResponseModel objModel = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                            LoginModel responseData = JsonConvert.DeserializeObject<LoginModel>(objModel.Response);

                            ViewBag.RegisterMessage = responseData.UserName + responseData.Password;
                            ReturnJson = "[{\"Response\":\"true \"}]";
                            SendMail.SendMailContact(UserEmailId, "Forget Password ", responseData.UserName, responseData.Password);
                            //return RedirectToAction("Index");

                        }
                    }

                }
                catch (Exception ex)
                {
                    String Exception = ex.ToString();
                }


            }

            return Json(ReturnJson);
        }

        private List<SubscriptionPlanModel> GetSubscriptionPlan()
        {
            SubscriptionPlanModel productSaveModel = new SubscriptionPlanModel();
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetSubscriptionPlan, "POST", _request);
            //if (_response == null) return View();
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

                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                    _data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json["unique_name"].ToString());
                    if (_data.ContainsKey("Response"))
                    {
                        return JsonConvert.DeserializeObject<List<SubscriptionPlanModel>>(_data["Response"].ToString());
                    }
                }
                return new List<SubscriptionPlanModel>();
            }
        }

        public ActionResult Subscribe()
        {
            ListPlan = GetSubscriptionPlan();
            ViewBag.SubscriptionPlan = new SelectList(ListPlan, "PlanId", "PlanName"); ;
            return View();
        }
        [HttpPost]
        public ActionResult SubscribePlan(FormCollection formColl, SubscribeModel objModel)
        {
            ListPlan = GetSubscriptionPlan();

            objModel.PlanAmount = ListPlan.Find(x => x.PlanId == objModel.PlanId).PlanAmount;
            objModel.PlanName = ListPlan.Find(x => x.PlanId == objModel.PlanId).PlanName;
            objModel.Currency = ListPlan.Find(x => x.PlanId == objModel.PlanId).Currency;
            //ActionResult ActResult = PaymentWithPaypal(null, objModel.PlanId, objModel.PlanAmount, objModel.Currency);

            return View();
        }

        #region PayPal Integrate

        [HttpPost]
        public ActionResult PaymentWithPaypal(FormCollection formColl, SubscribeModel objModel)
        {

            string Cancel = null;
            #region GetPlain Details

            ListPlan = GetSubscriptionPlan();
            if (objModel.PlanId == 0)
            {
                ListPlan = GetSubscriptionPlan();
                ViewBag.SubscriptionPlan = new SelectList(ListPlan, "PlanId", "PlanName"); ;

                return View("Subscribe");
            }
            long PlanId = objModel.PlanId;
            decimal planAmount = ListPlan.Find(x => x.PlanId == objModel.PlanId).PlanAmount;
            decimal payAmount = planAmount;
            decimal discount = 0;
            String planName = ListPlan.Find(x => x.PlanId == objModel.PlanId).PlanName;
            String currency = ListPlan.Find(x => x.PlanId == objModel.PlanId).Currency;
            #endregion


            //getting the apiContext
            APIContext apiContext = PayPalConfiguration.GetAPIContext();

            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Home/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, PlanId, planAmount, currency, planName);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("_partialViewPaypalPaymentFailure");
                    }
                }
            }
            catch (Exception ex)
            {
                string Exception = ex.ToString();
                return View("_partialViewPaypalPaymentFailure");
            }

            bool SaveDBSubscribe = SaveUserSubscriptionPlan(PlanId, planAmount, payAmount, discount, currency);
            UserModel MdUser = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager);
            String toMailId = MdUser.Email;
            String Subject = "Thanks For Payment Your Payment Is Pay " + payAmount;
            SendMail.SendMailContact(toMailId, Subject, MdUser.UserName, "");
            //on successful payment, show success page to user.
            return View("_ParitalViewPaypalPaymentSuccess");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, long PlanId, decimal planAmount, String currency, String itemName)
        {

            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            //Adding Item Details like name, currency, price etc
            itemList.items.Add(new Item()
            {
                name = itemName,
                currency = currency,//"USD"
                price = planAmount.ToString(),
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = planAmount.ToString()
            };
            Double TotalAmount = Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal);
            //Final amount with details
            var amount = new Amount()
            {
                currency = currency,// "USD"
                total =TotalAmount.ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your invoice number", //Generate an Invoice No
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        public ActionResult PaypalPaymentSuccess()
        {
            return View();
        }
        public ActionResult PaypalPaymentFailure()
        {
            return View();
        }
        #endregion

       

        private bool SaveUserSubscriptionPlan(long PlanId, decimal planAmount, decimal PayAmount, decimal discount, String Currency)
        {
            SubscribeModel objModel = new SubscribeModel();
            objModel.PlanId = PlanId;
            objModel.UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;
            objModel.PlanAmount = planAmount;
            objModel.PlanDiscount = PayAmount;
            objModel.PlanDiscount = discount;
            objModel.Currency = Currency;
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiSaveUserSubscriptionPlan, "POST", _request);
            if (_response == null)
                return false;
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {   //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                var jsonData = JsonConvert.SerializeObject(_data);
                if (json.ContainsKey("unique_name"))
                {
                    return true;
                }

            }
            return false;
        }

        public ActionResult DomainExpire()
        {
            return View();
        }
    }
}