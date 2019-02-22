using Amazonweb.Models;
using Amazonweb.web.Handlers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Amazonweb.web
{
    public class RouteConfig
    {
        static JwtTokenManager _JwtTokenManager = new JwtTokenManager();
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute("IUrlRouteHandler", "{*urlRouteHandler}").RouteHandler = new UrlRouteHandler();
            #region Comment Code 
            /*
            #region TemplateRuting Define

            #region Template 1 Routing

            routes.MapPageRoute("Templates1-index", "index/1userName/{username}/{id}", "~/TemplateThemes/Templete1/index.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates1-about", "about/1userName/{username}/{id}", "~/TemplateThemes/Templete1/about.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates1-contact", "contact/1userName/{username}/{id}", "~/TemplateThemes/Templete1/contact.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates1-allProducts", "allproducts/1userName/{username}/{id}", "~/TemplateThemes/Templete1/allproducts.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });

            routes.MapPageRoute("Templates1-singleProduct", "singleproduct/1userName/{username}/{id}/{asin}", "~/TemplateThemes/Templete1/single.html", false,
    new RouteValueDictionary(),
    new RouteValueDictionary { { "controller", "" } });
            #endregion
            #region Template 2 Routing
            routes.MapPageRoute("Templates2-index", "index/2userName/{username}/{id}", "~/TemplateThemes/Templete2/index.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates2-about", "about/2userName/{username}/{id}", "~/TemplateThemes/Templete2/about.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates2-contact", "contact/2userName/{username}/{id}", "~/TemplateThemes/Templete2/contact.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates2-allProducts", "allproducts/2userName/username/{id}", "~/TemplateThemes/Templete2/allproducts.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });

            #endregion
            #region Template 3 Routing
            routes.MapPageRoute("Templates3-index", "3userName/{id}", "~/TemplateThemes/Templete3/index.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates3-about", "3userName/{id}", "~/TemplateThemes/Templete3/about.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates3-contact", "3userName/{id}", "~/TemplateThemes/Templete3/contact.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });
            routes.MapPageRoute("Templates3-allProducts", "3userName/{id}", "~/TemplateThemes/Templete3/allproducts.html", false,
     new RouteValueDictionary(),
     new RouteValueDictionary { { "controller", "" } });

            #endregion
            #endregion


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "UrlRewrite",
               url: "{controller}/{action}/{username}/{id}",
               defaults: new { controller = "UrlRewrite", action = "Index", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "Redirectpage",
               url: "{controller}/{action}/{username}/{id}/{pagename}",
               defaults: new { controller = "UrlRewrite", action = "Index", id = UrlParameter.Optional }
           );
            //routes.MapPageRoute("SalesSummaryRoute","SalesReportSummary/{locale}", "~/sales.aspx");
            
             */
            #endregion
        }



        //private Guid userId()
        //{
        // Guid userId=   Services.GetLoginUser(this.ControllerContext.HttpContext, _JwtTokenManager).Id;
        //}
        //public static List<UserTemplateActivate> GetActiveTemplate(UserTemplateActivate objUserTemplate)
        //{
        //    var TemplateList = new List<UserTemplateActivate>();
        //    var _request = _JwtTokenManager.GenerateToken(JsonConvert.SerializeObject(objUserTemplate));

        //    var _response = Services.GetApiResponse(Constant.ApiGetActiveTemplate, "POST", _request);
        //    //  if (_response == null) return View();
        //    //---------- Get Api response stream
        //    using (var _result = new StreamReader(_response.GetResponseStream()))
        //    {

        //        //----------REturn response
        //        dynamic _data = _JwtTokenManager.DecodeToken(JsonConvert.DeserializeObject<ResponseModel>(_result.ReadToEnd()).Response);
        //        var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);


        //        if (json.ContainsKey("unique_name"))
        //        {
        //            ResponseModel RspUnique = JsonConvert.DeserializeObject<ResponseModel>(json["unique_name"].ToString());
        //            dynamic rspData = _JwtTokenManager.DecodeToken(RspUnique.Response);
        //            var jsonRspData = JsonConvert.DeserializeObject<Dictionary<string, object>>(rspData);

        //            TemplateList = JsonConvert.DeserializeObject<List<UserTemplateActivate>>(jsonRspData["unique_name"].ToString());
        //            // if (!login.Success) { ViewBag.Message = login.Response; return View(); }
        //            //var jsonString = new { Id = login.Id, UserName = login.Response }; \\ not cretae a correct Formate json

        //        }

        //    }
        //    return TemplateList;

        //}
    }
}
