using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Services
{
    public class UserServices : IUserServices
    {
        public IUserRepositary _instance { get; set; }
        public UserServices()
        {

        }
        public UserServices(IUserRepositary instance)
        {
            _instance = instance;
        }

        public ResponseModel RegisterUser(UserModel userModel)
        {
            var _result = _instance.RegisterUser(userModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel Login(LoginModel loginModel)
        {
            var _result = _instance.LoginAuthenticte(loginModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value ,Id= _result.Id.Value};
        }

        public ResponseModel SaveProduct(UserProductModel user)
        {
           // user.Operation = "insert";
            var _result = _instance.SaveProduct(user);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel UserProducts(UserProductModel user)
        {
            var _result = _instance.GetUserProducts(user);
            return new ResponseModel() { Response = JsonConvert.SerializeObject( _result) ,Success=true};
        }


        public object GetUserDetailByUserId(UserProductModel user)
        {
           // var _result = null;// _instance.GetUserDetailByUserId(user);
            return new ResponseModel();// { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel ChangePassword(ChangePasswordModel objModel)
        {
            var _result = _instance.ChangePassword(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel ForgetPassword(LoginModel objModel)
        {
            var _result = _instance.ForgetPassword(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success =true};
        }

        public ResponseModel GetUserSubscriptionPlan(UserSubscriptionPlanModel objModel)
        {
            var _result = _instance.GetUserSubscriptionPlan(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel GetSubscriptionPlan(SubscriptionPlanModel objModel)
        {
            var _result = _instance.GetSubscriptionPlan(objModel);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel SaveUserSubscriptionPlan(SubscribeModel objModel)
        {
            var _result = _instance.SaveUserSubscriptionPlan(objModel);
            return new ResponseModel() { Response = _result.Response, Success =true };
        }

        public ResponseModel GetAuthorize(LoginModel loginModel)
        {
            var _result = _instance.GetUserAuthorize(loginModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value, Id =_result.Id };
        }

        public ResponseModel SaveUserTemplate(UserTemplateModel UserTemplateActiveModel)
        {
            var _result = _instance.SaveUserTemplate(UserTemplateActiveModel);
            return new ResponseModel() { Response=_result.Response, Success = _result.Success.Value };
        }

        public ResponseModel GetUserTemplates(UserTemplateModel UserTemplateActiveModel)
        {
            var _result = _instance.GetUserTemplates(UserTemplateActiveModel);
            return new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(_result)), Success = true };
        }

        public ResponseModel GetUserActiveTemplate(UserTemplateModel UserTemplateActiveModel)
        {
            var _result = _instance.GetUserActiveTemplate(UserTemplateActiveModel);
            return new ResponseModel() { Response = _result.TemplateId.ToString(), Success = true };
           // return new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(_result)), Success = true };

        }
    }

    public interface IUserServices
    {
        ResponseModel RegisterUser(UserModel userModel);
        ResponseModel Login(LoginModel loginModel);
        ResponseModel GetAuthorize(LoginModel loginModel);
        ResponseModel SaveProduct(UserProductModel user);
        object GetUserDetailByUserId(UserProductModel user);
        ResponseModel UserProducts(UserProductModel user);
        ResponseModel ChangePassword(ChangePasswordModel objModel);
        ResponseModel ForgetPassword(LoginModel objModel);
        ResponseModel GetUserSubscriptionPlan(UserSubscriptionPlanModel objModel);
        ResponseModel GetSubscriptionPlan(SubscriptionPlanModel objModel);
        ResponseModel SaveUserSubscriptionPlan(SubscribeModel objModel);

        ResponseModel SaveUserTemplate(UserTemplateModel UserTemplateActiveModel);
        ResponseModel GetUserTemplates(UserTemplateModel UserTemplateActiveModel);
        ResponseModel GetUserActiveTemplate(UserTemplateModel UserTemplateActiveModel);

    }
}