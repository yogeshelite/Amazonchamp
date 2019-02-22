using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using AmazonwebApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AmazonwebApi.Controllers
{
    public class AboutController : ApiController
    {
        private IAboutServices aboutServices;

        public AboutController()
        {
            aboutServices = new AboutServices(new AboutRepositary());
        }
        [HttpPost]
        [Route("api/About/SaveAbout")]

        public IHttpActionResult SaveAbout(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                AboutModel user = JsonConvert.DeserializeObject<AboutModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(aboutServices.SaveAbout(user))), Success = true });
            }
            //SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@Id", user.Id);
            //param[1] = new SqlParameter("@ASIN", user.ASIN);
            //param[2] = new SqlParameter("@isFeatured", user.isFeatured);
            //param[3] = new SqlParameter("@categoryId", user.categoryId);
            //var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "dbo.SaveProduct", System.Data.CommandType.StoredProcedure, param);

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }
        [HttpPost]
        [Route("api/About/GetAbout")]

        public IHttpActionResult GetProductCategory()
        {
            return Json(aboutServices.GetAbout());
        }

        [HttpPost]
        [Route("api/About/GetUserAbout")]

        public IHttpActionResult GetUserAbout(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                AboutModel user = JsonConvert.DeserializeObject<AboutModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(aboutServices.GetUserAbout(user))), Success = true });

               // return Json(aboutServices.GetUserAbout());
            }
            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }
    }
}
