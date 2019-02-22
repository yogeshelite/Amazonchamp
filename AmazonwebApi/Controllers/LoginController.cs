using AmazonwebApi.Helper;
using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using AmazonwebApi.Services;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Web.Http;


namespace AmazonwebApi.Controllers
{
    public class LoginController : ApiController
    {
        private IUserServices userServices;
        public LoginController(IUserServices _userServices)
        {
            userServices = _userServices;
        }

       
        //[System.Web.Http.HttpPost]
        //public IHttpActionResult ForgotPassword(ApiRequestModel requestModel)
        //{
        //    var data = new JwtTokenManager().DecodeToken(requestModel.Data);
        //    LoginModel loginModel = JsonConvert.DeserializeObject<LoginModel>(data);
        //    SqlParameter[] param = new SqlParameter[1];
        //    param[0] = new SqlParameter("@emailaddress", loginModel.ForgotPasswordEmailId);
        //    var dt = SqlHelper.ExecuteDataTable(HelperClass.ConnectionString, "USP_VerifyEmailAddress", System.Data.CommandType.StoredProcedure, param);
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        return Json(new { IsSuccess = false, data = "Email id is wrong." });
        //    }
        //    else
        //    {
        //        var path = AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\ForgotPassword.html";
        //        var bodyOfMail = "";
        //        using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
        //        {
        //            bodyOfMail = reader.ReadToEnd();
        //        }

        //        bodyOfMail = bodyOfMail.Replace("[FirstName]", dt.Rows[0]["FirstName"].ToString());
        //        bodyOfMail = bodyOfMail.Replace("[Password]", dt.Rows[0]["Password"].ToString());
        //        // Sending Email
        //        EmailHelper objHelper = new EmailHelper();
        //        objHelper.SendEMail(loginModel.ForgotPasswordEmailId, HelperClass.ForgotPasswordEmailSubject, bodyOfMail);
        //    }

        //    //  return Json(new { IsSuccess = true, data = "Password sent.", Token = JsonConvert.SerializeObject(param) });
        //    return Json(new { IsSuccess = true, data = "Password sent."});

        //}
    }
}
