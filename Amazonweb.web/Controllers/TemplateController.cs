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
    public class TemplateController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        // GET: ActiveTemplate
        public ActionResult TemplateList()
        {
            var TemplateList = GetListTemplate();
            ViewBag.TemplateList = TemplateList;
            //ViewBag.TemplateList = new SelectList(TemplateList, "TemplateId", "TemplateName");

            ActiveTemplate objmodel = new ActiveTemplate();
            //ViewBag.JavaScriptFunction = string.Format("JsFunTemplateActivate('{0}');", 1);
            UserTemplate objUserTemplate = new UserTemplate();
            objUserTemplate.UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;
            string userName= Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).UserName;
            var ActiveTemplate = GetUserTemplate(objUserTemplate);
            String TemplateId = "";
            if (ActiveTemplate != null)
            {
                TemplateId = ActiveTemplate[0].TemplateId.ToString();
            }
            ViewBag.activeTemplateId = TemplateId;
            //ViewBag.shareUrl = Constant.AppDomainName + "temp" + TemplateId + "/index/" + objUserTemplate.UserId;
            ViewBag.shareUrl = Constant.AppDomainName + "/UrlRewrite/Index/" + userName+"/"+ objUserTemplate.UserId+"/index";
            return View(objmodel);
        }

        public List<UserTemplate> GetUserTemplate(UserTemplate objUserTemplate)
        {
            var TemplateList = new List<UserTemplate>();
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objUserTemplate));

            var _response = Services.GetApiResponse(Constant.ApiGetUserTemplates, "POST", _request);
            //  if (_response == null) return View();
            //---------- Get Api response stream
            using (var _result = new StreamReader(_response.GetResponseStream()))
            {

                //----------REturn response
                dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);


                if (json.ContainsKey("unique_name"))
                {
                    ResponseModel RspUnique = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                    dynamic rspData = _JwtTokenManager.DecodeToken(RspUnique.Response);
                    var jsonRspData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rspData);

                    TemplateList = JsonConvert.DeserializeObject<List<UserTemplate>>(jsonRspData["unique_name"].ToString());
                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json

                }

            }
            return TemplateList;

        }

        public List<TemplateModel> GetListTemplate()
        {
            var TemplateList = new List<TemplateModel>();
            var _request = "";//_JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));

            var _response = Services.GetApiResponse(Constant.ApiGetTemplates, "POST", _request);
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
                    TemplateList = JsonConvert.DeserializeObject<List<TemplateModel>>(json["unique_name"].ToString());
                    // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
                    //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json

                }

            }
            return TemplateList;

        }
        // GET: ActiveTemplate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ActiveTemplate/Create
        public ActionResult Create()
        {
            return View();
        }
        [Route("Template/SetTemplateActive")]
        public ActionResult SetTemplateActive(int TemplateId,bool IsActive )
        {

            Guid UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;
            UserTemplate objUserTemplateActive = new UserTemplate();
            objUserTemplateActive.TemplateId = TemplateId;
            objUserTemplateActive.UserId = UserId;
            objUserTemplateActive.Active = IsActive;
            try
            {

                var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objUserTemplateActive));


                //----------Call Api
                var _response = Services.GetApiResponse(Constant.ApiSaveUserTemplate, "POST", _request);
                if (_response == null) return View(objUserTemplateActive);
                //---------- Get Api response stream
                using (var _result = new StreamReader(_response.GetResponseStream()))
                {

                    //----------REturn response
                    dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                    var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                    if (json.ContainsKey("unique_name"))
                    {
                        ResponseModel retResponse = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                        if (!retResponse.Success) { ViewBag.Message = retResponse.Response; return View("TemplateList", objUserTemplateActive); }
                        return RedirectToAction("TemplateList");
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return View("Login", objUserTemplateActive);

            // return RedirectToAction("Index");
            //return View("Login", objUserTemplateActive);


        }

        // POST: ActiveTemplate/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            long TemplateId = 1;
            Guid UserId = Guid.Empty;
            UserTemplate objUserTemplateActive = new UserTemplate();
            objUserTemplateActive.TemplateId = TemplateId;
            objUserTemplateActive.UserId = UserId;
            if (ModelState.IsValid)
            {
                try
                {

                    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objUserTemplateActive));


                    //----------Call Api
                    var _response = Services.GetApiResponse(Constant.ApiSaveUserTemplate, "POST", _request);
                    if (_response == null) return View(objUserTemplateActive);
                    //---------- Get Api response stream
                    using (var _result = new StreamReader(_response.GetResponseStream()))
                    {

                        //----------REturn response
                        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                        if (json.ContainsKey("unique_name"))
                        {
                            ResponseModel retResponse = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                            if (!retResponse.Success) { ViewBag.Message = retResponse.Response; return View("TemplateList", objUserTemplateActive); }
                            return RedirectToAction("TemplateList");

                        }

                    }

                }
                catch (Exception ex)
                {

                }
                return View("Login", objUserTemplateActive);
            }
            // return RedirectToAction("Index");
            return View("Login", objUserTemplateActive);


        }

        // GET: ActiveTemplate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ActiveTemplate/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ActiveTemplate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ActiveTemplate/Delete/5
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
    }
}
