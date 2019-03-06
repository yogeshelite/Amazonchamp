using AmazonwebApi.Helper;
using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using AmazonwebApi.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
//using System.Web.Mvc;

namespace AmazonwebApi.Controllers
{

    public class UserController : ApiController
    {
        private IUserServices userServices;

        public UserController()
        {
            userServices = new UserServices(new UserRepositary());
        }

        [HttpPost]
        [Route("api/User/AuthenticateUser")]
        public IHttpActionResult AuthenticateUser(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                LoginModel login = JsonConvert.DeserializeObject<LoginModel>(request["unique_name"].ToString());

                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.Login(login))), Success = true });
            }
            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

        [HttpPost]
        [Route("api/User/GetUserAuthorize")]
        public IHttpActionResult GetUserAuthorize(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                LoginModel login = JsonConvert.DeserializeObject<LoginModel>(request["unique_name"].ToString());

                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.GetAuthorize(login))), Success = true });
            }
            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

        [HttpPost]
        [Route("api/User/ForgetPassword")]
        public IHttpActionResult ForgetPassword(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                LoginModel login = JsonConvert.DeserializeObject<LoginModel>(request["unique_name"].ToString());

                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.ForgetPassword(login))), Success = true });
            }
            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

        [Route("api/User/Registeration")]
        [HttpPost]
        public IHttpActionResult Registeration(RequestModel requestModel)
        {
            // = JsonConvert.DeserializeObject<ApiRequestModel>(System.Web.HttpContext.Current.Request.Form["encrypted"].ToString());

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserModel user = JsonConvert.DeserializeObject<UserModel>(request["unique_name"].ToString());

                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.RegisterUser(user))), Success = true });
            }
            //var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\RegistrationTemplate.html";
            //var bodyOfMail = "";
            //using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
            //{
            //    bodyOfMail = reader.ReadToEnd();
            //}

            //bodyOfMail = bodyOfMail.Replace("[FirstName]", user.FirstName.ToString());
            //bodyOfMail = bodyOfMail.Replace("[UserName]", user.UserName.ToString());
            //bodyOfMail = bodyOfMail.Replace("[Password]", password);
            //bodyOfMail = bodyOfMail.Replace("[LoginUrl]", HelperClass.LoginURL);
            //// Sending Email
            //EmailHelper objHelper = new EmailHelper();
            //objHelper.SendEMail(user.EmailAddress, HelperClass.RegistrationEmailSubject, bodyOfMail);

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });


        }



        //[HttpPost]
        //public IHttpActionResult UpdateUser(ApiRequestModel requestModel)
        //{
        //    var data = new JwtTokenManager().DecodeToken(requestModel.Data);
        //    UserModel user = JsonConvert.DeserializeObject<UserModel>(data);
        //    SqlParameter[] param = new SqlParameter[2];
        //    param[0] = new SqlParameter("@UserId", user.UserId);
        //    param[1] = new SqlParameter("@Password", user.Password);
        //    var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_CheckIfPasswordIsCorrect", System.Data.CommandType.StoredProcedure, param);
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        return Json(new { IsSuccess = false, Message = "Old password is not correct." });
        //    }

        //    param = new SqlParameter[5];
        //    param[0] = new SqlParameter("@FirstName", user.FirstName);
        //    param[1] = new SqlParameter("@SurName", user.SurName);
        //    param[2] = new SqlParameter("@NameOfCompany", user.NameOfCompany);
        //    param[3] = new SqlParameter("@Password", user.NewPassword);
        //    param[4] = new SqlParameter("@UserId", user.UserId);
        //    SqlHelper.ExecuteScalar(HelperClass.ConnectionString, "USP_UpdateUser", System.Data.CommandType.StoredProcedure, param);

        //    param = new SqlParameter[1];
        //    param[0] = new SqlParameter("@UserId", user.UserId);
        //    dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_GetUserById", System.Data.CommandType.StoredProcedure, param);

        //    // Sending Email on Password updated.
        //    var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\PasswordUpdated.html";
        //    var bodyOfMail = "";
        //    using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
        //    {
        //        bodyOfMail = reader.ReadToEnd();
        //    }

        //    bodyOfMail = bodyOfMail.Replace("[FirstName]", user.FirstName.ToString());
        //    bodyOfMail = bodyOfMail.Replace("[LoginUrl]", HelperClass.LoginURL);
        //    // Sending Email
        //    EmailHelper objHelper = new EmailHelper();
        //    objHelper.SendEMail(user.EmailAddress, HelperClass.PasswordUpdatedEmailSubject, bodyOfMail);

        //    return Json(new { IsSuccess = true, Message = "Updated", data = JsonConvert.SerializeObject(dt) });
        //    //  return Json(new { IsSuccess = true, data = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(HelperClass.DataTableToJSONWithJavaScriptSerializer(dt))) });

        //}


        //private static 
        string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [HttpPost]
        [Route("api/User/GetUserDetailByUserId")]

        public IHttpActionResult GetUserDetailByUserId(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserProductModel user = JsonConvert.DeserializeObject<UserProductModel>(new JwtTokenManager().DecodeToken(requestModel.Data));
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.GetUserDetailByUserId(user))), Success = true });
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
        [Route("api/User/SaveProduct")]

        public IHttpActionResult SaveProduct(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserProductModel user = JsonConvert.DeserializeObject<UserProductModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.SaveProduct(user))), Success = true });
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
        [Route("api/User/UserProducts")]

        public IHttpActionResult GetUserProducts(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserProductModel user = JsonConvert.DeserializeObject<UserProductModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.UserProducts(user))), Success = true });
            }
            //SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@Id", user.Id);
            //param[1] = new SqlParameter("@ASIN", user.ASIN);
            //param[2] = new SqlParameter("@isFeatured", user.isFeatured);
            //param[3] = new SqlParameter("@categoryId", user.categoryId);
            //var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "dbo.SaveProduct", System.Data.CommandType.StoredProcedure, param);

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }


        //// DELETE: api/User/5
        ////[ResponseType(typeof(tblUser))]
        //[HttpDelete]
        //public IHttpActionResult DeletetblUser(int id)
        //{
        //    tblUser tblUser = db.tblUsers.Find(id);
        //    if (tblUser == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tblUsers.Remove(tblUser);
        //    db.SaveChanges();

        //    return Ok(tblUser);
        //}

     

        //private bool tblUserExists(int id)
        //{

        //    return db.tblUsers.Count(e => e.UserId == id) > 0;
        //}


        [HttpPost]
        [Route("api/User/SaveTemplate")]
        public IHttpActionResult SaveTemplate(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserTemplateModel UserTemplateActModel = JsonConvert.DeserializeObject<UserTemplateModel>(request["unique_name"].ToString());

                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.SaveUserTemplate(UserTemplateActModel))), Success = true });
            }
            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

        [HttpPost]
        [Route("api/User/GetTemplates")]
        public IHttpActionResult GetTemplates(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserTemplateModel UserTemplateActModel = JsonConvert.DeserializeObject<UserTemplateModel>(request["unique_name"].ToString());
                var jsonValueret = Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.GetUserTemplates(UserTemplateActModel))), Success = true });
                return jsonValueret;
            }
            var jsonValue = Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
            return jsonValue;
        }

        [HttpPost]
        [Route("api/User/GetUserActiveTemplate")]
        public IHttpActionResult GetUserActiveTemplate(RequestModel requestModel)
        {

            var data = new JwtTokenManager().DecodeToken(requestModel.Data);
            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserTemplateModel UserTemplateActModel = JsonConvert.DeserializeObject<UserTemplateModel>(request["unique_name"].ToString());
                var DbResponse= userServices.GetUserActiveTemplate(UserTemplateActModel);
                var jsonValueret = Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(DbResponse)), Success = true });
                return jsonValueret;
            }
            var jsonValue = Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
            return jsonValue;
        }




        [HttpPost]
        [Route("api/User/ChangePassword")]

        public IHttpActionResult ChangePassword(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                ChangePasswordModel objModel = JsonConvert.DeserializeObject<ChangePasswordModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.ChangePassword(objModel))), Success = true });
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
        [Route("api/User/GetUserSubscriptionPlan")]

        public IHttpActionResult GetUserSubscriptionPlan(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                UserSubscriptionPlanModel objModel = JsonConvert.DeserializeObject<UserSubscriptionPlanModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.GetUserSubscriptionPlan(objModel))), Success = true });
            }

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }
        [HttpPost]
        [Route("api/User/GetSubscriptionPlan")]

        public IHttpActionResult GetSubscriptionPlan(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                SubscriptionPlanModel objModel = JsonConvert.DeserializeObject<SubscriptionPlanModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.GetSubscriptionPlan(objModel))), Success = true });
            }

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }

        [HttpPost]
        [Route("api/User/SaveUserSubscriptionPlan")]

        public IHttpActionResult SaveUserSubscriptionPlan(RequestModel requestModel)
        {
            var data = new JwtTokenManager().DecodeToken(requestModel.Data);

            Dictionary<string, object> request = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
            if (request.ContainsKey("unique_name"))
            {
                SubscribeModel objModel = JsonConvert.DeserializeObject<SubscribeModel>(request["unique_name"].ToString());
                return Json(new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(userServices.SaveUserSubscriptionPlan(objModel))), Success = true });
            }
            //SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@Id", user.Id);
            //param[1] = new SqlParameter("@ASIN", user.ASIN);
            //param[2] = new SqlParameter("@isFeatured", user.isFeatured);
            //param[3] = new SqlParameter("@categoryId", user.categoryId);
            //var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "dbo.SaveProduct", System.Data.CommandType.StoredProcedure, param);

            return Json(new ResponseModel() { Response = BadRequest().ToString(), Success = false });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }


    }
}