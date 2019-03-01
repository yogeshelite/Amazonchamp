
using Amazonweb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Amazonweb.web.Handlers
{
    public sealed class UrlHandler
    {
        static JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        public static UrlRouteData GetRoute(string url)
        {
            url = url ?? "/";
            url = url == "/" ? "" : url;
            url = url.ToLower();
             
            UrlRouteData urlRoute = null;
            if (!string.IsNullOrEmpty(url.Trim()) )
            {
                var urlsegment = GetUrlSegments(url);
                if (url.Length > 3 && url.Substring(0, 2).Equals("u/"))
                {
                    if (urlsegment.Length >= 1)
                    {
                        var template = GetUserActiveTemplate(new UserTemplate() { UserName = urlsegment[1] });
                        if (template != null)
                        {
                            HttpCookie userName = new HttpCookie("userName", urlsegment[1]);
                            userName.Expires = DateTime.Now.AddDays(-1);
                            HttpContext.Current.Response.Cookies.Add(userName);
                            return new UrlRouteData()
                            {
                                Id = new Random().Next(),
                                Controller = template.TemplateName,
                                Action = (urlsegment.Length > 2) ? urlsegment[2] : "Index",
                                Url = urlsegment[1]
                            };
                        }
                    }
                }
                else
                {
                    return new UrlRouteData()
                    {
                        Id = new Random().Next(),
                        Controller = urlsegment[0],
                        Action = (urlsegment.Length > 1) ? urlsegment[1] : "Index",
                        Url = urlsegment[0]
                    };

                }
            }
            else
            {

                return new UrlRouteData()
                {
                    Id = new Random().Next(),
                    Controller = "Home",
                    Action = "Login",
                    Url = "Login"
                };
            }







            return urlRoute;
        }

      

        public static UserTemplate GetUserActiveTemplate(UserTemplate objUserTemplate)
        {
            List< UserTemplate> TemplateList=null;
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

            return (TemplateList == null)?null: TemplateList.FirstOrDefault(f=>f.Active);

        }
        private static RouteData GetControllerActionFromUrl(string url)
        {
            var route = new RouteData();

            if (!string.IsNullOrEmpty(url))
            {
                var segments = url.Split('/');
                if (segments.Length >= 1)
                {
                    route.Id = new Random().Next()  ;
                    route.Controller = segments[0];
                    route.Action = route.Id == 0? (segments.Length >= 2? segments[1] : route.Action) : route.Action;
                }
            }

            return route;
        }

        private static string[] GetUrlSegments(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var segments = url.Split('/');
                if (segments.Length >= 1)
                {
                    
                    return segments;
                }
            }

            return null;
        }
    }
}