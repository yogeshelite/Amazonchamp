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
    public class UrlRewriteController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        // GET: UrlRewrite
        public ActionResult Index()
        {

            string prmUserName = Request.Url.Segments[3];
            string userName = prmUserName.Substring(0, prmUserName.Length - 1);
            string prmuserid = Request.Url.Segments[4];
            Guid prmUserId = Guid.Parse(prmuserid.Substring(0, prmuserid.Length - 1));
            bool isAuthorize = UserAuthorize(userName, prmUserId);
            if (isAuthorize == false)
            {
                return RedirectToAction("DomainExpire","Home");
            }
            string pageName = "";
            if (Request.Url.Segments[5] == null)
                pageName = "index";
            else
                pageName = Request.Url.Segments[5];

            UserTemplate objUserTemplate = new UserTemplate();
            //objUserTemplate.UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;
            objUserTemplate.UserId = prmUserId;

            //string userName= Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).UserName;
            //string userName = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).UserName;

            var ActiveTemplate = GetUserTemplate(objUserTemplate);
            String TemplateId = "";
            if (ActiveTemplate != null)
            {
                TemplateId = ActiveTemplate[0].TemplateId.ToString();
            }
            String TemplateName = "";
            string pageRoute = "";
            switch (pageName)
            {
                case "index":
                    pageRoute = "index";
                    break;
                case "about":
                    pageRoute = "about";
                    break;
                case "contact":
                    pageRoute = "contact";
                    break;
                case "allproducts":
                    pageRoute = "allproducts";
                    break;
                case "singleproduct":
                    pageRoute = "singleproduct";
                    break;
                default:
                    pageRoute = "";
                    break;
            }
            // string Url = Constant.AppDomainName + "temp" + TemplateId + "/index/" + objUserTemplate.UserId;
            string Url = Constant.AppDomainName + pageRoute + "/" + TemplateId + "userName/" + userName + "/" + objUserTemplate.UserId;

            return Redirect(Url);
            //return RedirectToRoute(TemplateName, new { id = objUserTemplate.UserId });
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

        private bool UserAuthorize(string username, Guid userId)
        {
            try
            {
                LoginModel loginModel = new LoginModel();
                loginModel.UserName = username;
                loginModel.Id = userId;

                var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(loginModel));


                //----------Call Api
                var _response = Services.GetApiResponse(Constant.ApiGetUserAuthorize, "POST", _request);
                using (var _result = new StreamReader(_response.GetResponseStream()))
                {

                    //----------REturn response
                    dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
                    var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
                    if (json.ContainsKey("unique_name"))
                    {
                        ResponseModel login = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
                        if (!login.Success)
                        {
                            ViewBag.Message = login.Response;
                            return false;
                        }
                        //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json
                        String VarResponse = login.Response;
                        string[] ArrResponse = VarResponse.Split(',');
                        var jsonString = "{\"Id\":\"" + login.Id + "\",\"UserName\":\"" + ArrResponse[0] + "\",\"Email\":\"" + ArrResponse[1] + "\"}";
                        return true;

                    }

                }

            }
            catch (Exception ex)
            {
                return false;

            }
            return false;
        }

    }
}