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
    public class KeywordResearchController : ApiController
    {
        private IKeywordResearchServices KeywordResearchServices;

        public KeywordResearchController()
        {
            KeywordResearchServices = new KeywordResearchServices(new KeywordResearchRepositary());
        }
        [HttpPost]
        [Route("api/User/Savekeywords")]

        public IHttpActionResult Savekeywords(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                KeyWordResearchModel objmodel = JsonConvert.DeserializeObject<KeyWordResearchModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(KeywordResearchServices.SaveKeyword(objmodel))), Success = true });
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
        [Route("api/User/GetKeyword")]

        public IHttpActionResult GetKeyword(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                KeyWordResearchModel objModel = JsonConvert.DeserializeObject<KeyWordResearchModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(KeywordResearchServices.GetKeyword(objModel))), Success = true });
            }
            //SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@Id", user.Id);
            //param[1] = new SqlParameter("@ASIN", user.ASIN);
            //param[2] = new SqlParameter("@isFeatured", user.isFeatured);
            //param[3] = new SqlParameter("@categoryId", user.categoryId);
            //var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "dbo.SaveProduct", System.Data.CommandType.StoredProcedure, param);

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

    }
}
