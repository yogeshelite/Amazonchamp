using Amazonweb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.Controllers
{
    public class UserAboutController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        List<UserAboutModel> AboutList;
        // GET: UserAbout
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutsList()
        {
            //AboutList = GetAbouts();
            String userId = Convert.ToString(Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id);
            AboutList = GetUserAboutList(userId);
            return View(AboutList);

        }
        private List<UserAboutModel> GetAbouts()
        {
            UserAboutModel userAboutModel = new UserAboutModel()
            {
                UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            };
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(userAboutModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetUserAbout, "POST", _request);
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
                    return _data = JsonConvert.DeserializeObject<List<UserAboutModel>>(json["unique_name"].ToString());
                    //if (_data.ContainsKey("Response"))
                    //{
                    //    return JsonConvert.DeserializeObject<List<UserAboutModel>>(_data["Response"].ToString());
                    //}
                }
                return new List<UserAboutModel>();
            }
        }
        // GET: UserAbout/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserAbout/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: UserAbout/Create
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];

                    String FileName = SaveImage(FileUpload);
                    SaveUpdateAboutUser(collection, FileName);

                }
            }
            catch
            {

            }
            return View();
        }
        private void SaveUpdateAboutUser(FormCollection collection, String FileName)
        {
            UserAboutModel aboutModel = new UserAboutModel()
            {
                AboutTitle = collection["AboutTitle"].ToString(),
                AboutSummary = collection["AboutSummary"].ToString(),
                UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id,
                PhoneNo = collection["PhoneNo"],
                Address = collection["Address"].ToString(),
                Twitter = collection["Twitter"].ToString(),
                Facebook = collection["Facebook"].ToString(),
                Linkedin = collection["Linkedin"].ToString(),
               AttachmentLogoName= FileName
            };
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(aboutModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiSaveAbout, "POST", _request);
            if (_response == null)
                return;
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
                    // ProductCategoryList = JsonConvert.DeserializeObject<List<CateoryModel>>(json["unique_name"].ToString());
                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                    return;
                }

            }
        }

        // GET: UserAbout/Edit/5
        [Route("UserAbout/AboutsList/Edit")]
        public ActionResult Edit(int id)
        {
            String userId = Convert.ToString(Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id);

            AboutList = GetUserAboutList(userId);
            UserAboutModel objUserModel = new UserAboutModel();
            objUserModel.AboutTitle = AboutList[0].AboutTitle.ToString();
            objUserModel.AboutSummary = AboutList[0].AboutSummary.ToString();
            objUserModel.Address = AboutList[0].Address.ToString();
            objUserModel.PhoneNo = AboutList[0].PhoneNo.ToString();
            objUserModel.Twitter = AboutList[0].Twitter.ToString();
            objUserModel.Facebook = AboutList[0].Facebook.ToString();
            objUserModel.Linkedin = AboutList[0].Linkedin.ToString();
            return View(objUserModel);
        }

        // POST: UserAbout/Edit/5
        [HttpPost]
        [Route("UserAbout/AboutsList/Update")]
        [ValidateInput(false)]
        public ActionResult update(int id, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   HttpPostedFileBase FileUpload = Request.Files["FileUploadImage"];
                    String FileName = SaveImage(FileUpload);
                   SaveUpdateAboutUser(collection, FileName);

                }
            }
            catch(Exception ex)
            {
                String Exception = ex.ToString();
            }
            String userId = Convert.ToString(Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id);
            AboutList = GetUserAboutList(userId);
            return View("AboutsList", AboutList);
        }
        private String SaveImage(HttpPostedFileBase FileUpload)
        {
            string filename = FileUpload.FileName;
            string targetpath = Server.MapPath("~/Doc/");
            string Extention = Path.GetExtension(filename);
            string DynamicFileName = Guid.NewGuid().ToString() + Extention;
            FileUpload.SaveAs(targetpath + DynamicFileName);
            return DynamicFileName;
        }
        // GET: UserAbout/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserAbout/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


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

        public List<UserAboutModel> GetUserAboutList(String UserId)
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
                        Dictionary<string, object> objListAboutModelst = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonDeser["unique_name"].ToString());
                        UserAboutModel objuserAboutModel = new UserAboutModel();
                        objuserAboutModel.Id = Convert.ToInt64(objListAboutModelst.Values.ElementAt(0).ToString());
                        objuserAboutModel.AboutTitle = objListAboutModelst.Values.ElementAt(1).ToString();
                        objuserAboutModel.AboutSummary = objListAboutModelst.Values.ElementAt(2).ToString();
                       
                        objuserAboutModel.Address = objListAboutModelst.Values.ElementAt(3).ToString();
                        objuserAboutModel.PhoneNo = objListAboutModelst.Values.ElementAt(4).ToString();
                        objuserAboutModel.Twitter = objListAboutModelst.Values.ElementAt(5).ToString();
                        objuserAboutModel.Facebook = objListAboutModelst.Values.ElementAt(6).ToString();
                        objuserAboutModel.Linkedin = objListAboutModelst.Values.ElementAt(7).ToString();
                        string FilePath = "~/Doc/"+ objListAboutModelst.Values.ElementAt(8).ToString();
                        objuserAboutModel.AttachmentLogoName =FilePath ;

                        List<UserAboutModel> userAboutModelsList = new List<UserAboutModel>();
                        userAboutModelsList.Add(objuserAboutModel);
                        //var listUserAbout = JsonConvert.DeserializeObject<List<UserAboutModel>>(ObjUserAbout);


                        //  var jSonValue= json(strserialize, JsonRequestBehavior.AllowGet);
                        return userAboutModelsList;
                    }

                }

                return new List<UserAboutModel>();
            }
        }

        public int SendMailContact(string receiverEmailId, string name, string message,string phone,string subject)
        {
            try
            {
                    var senderEmail = new MailAddress("xxxxxxxxxxx", name);
                    var receiverEmail = new MailAddress(receiverEmailId, "Receiver");
                var password ="xxxxxxxxxx";
                   var body = "<b>Phone N0 is</b>= "+phone +"<p> Text Message=== "+message+"</p>";
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
                        IsBodyHtml=true,
                        
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
    }
}
