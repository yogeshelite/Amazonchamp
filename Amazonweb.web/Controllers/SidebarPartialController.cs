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
    public class SidebarPartialController : Controller
    {
        JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        List<UserSubscriptionPlanModel> UserSubscribePlain;
        // GET: SidebarPartial
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult SidebarValue()
        {
            UserSubscribePlain = GetUserSubscriptionPlan();
            ViewBag.UserSubscribePlain = UserSubscribePlain;
            return PartialView("~\\Views\\Shared\\PartialView\\_SidebarPartial.cshtml");
        }
        private List<UserSubscriptionPlanModel> GetUserSubscriptionPlan()
        {
            UserSubscriptionPlanModel productSaveModel = new UserSubscriptionPlanModel()
            {
                UserId = Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id
            };
            var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(productSaveModel));
            // TODO: Add insert logic here
            var _response = Services.GetApiResponse(Constant.ApiGetUserSubscriptionPlan, "POST", _request);
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
                        return JsonConvert.DeserializeObject<List<UserSubscriptionPlanModel>>(_data["Response"].ToString());
                    }
                }
                return new List<UserSubscriptionPlanModel>();
            }
        }
    }
}