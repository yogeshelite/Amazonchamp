using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Persistance.Repositary
{
    public class UserRepositary : GenericRepository<amazonchampEntities1>, IUserRepositary
    {
        // private amazonchampEntities1 _dbContext { get; set; }
        //public UserRepositary(amazonchampEntities1 dbContext)
        //{
        //   _dbContext = dbContext;
        //}

        public RagisterUser_Result RegisterUser(UserModel userModel)
        {
            userModel.Id = Guid.NewGuid(); 
            return Context.RagisterUser(userModel.UserName, userModel.Password, userModel.Email, userModel.RegisterationDate).FirstOrDefault();

        }

        public LoginAuthenticate_Result LoginAuthenticte(LoginModel loginModel)
        {
            var RESULT = Context.LoginAuthenticate(loginModel.UserName, loginModel.Password, loginModel.RecordTime);
            return  RESULT.FirstOrDefault();
        }

        public SaveProduct_Result SaveProduct(UserProductModel user)
        {
            return Context.SaveProduct(user.ASIN, user.UserId, user.isFeatured, user.categoryId,user.Operation).FirstOrDefault();
        }

        public IEnumerable<GetUserProducts_Result> GetUserProducts(UserProductModel user)
        {
            return Context.GetUserProducts(user.UserId);
        }

        public ChangePassword_Result ChangePassword(ChangePasswordModel objModel)
        {
            return Context.ChangePassword(objModel.Id, objModel.OldPassword, objModel.NewPassword, objModel.ConfirmPassword).FirstOrDefault();
        }

        public ForgetPassword_Result ForgetPassword(LoginModel objModel)
        {
            return Context.ForgetPassword(objModel.UserName).FirstOrDefault();
        }

        public IEnumerable<GetUserSubscriptionPlan_Result> GetUserSubscriptionPlan(UserSubscriptionPlanModel objModel)
        {
            return Context.GetUserSubscriptionPlan(objModel.UserId);
        }

        public IEnumerable<GetSubscriptionPlan_Result> GetSubscriptionPlan(SubscriptionPlanModel objModel)
        {
            return Context.GetSubscriptionPlan();
        }

        public SaveUserSubscriptionPlan_Result SaveUserSubscriptionPlan(SubscribeModel objModel)
        {
            return Context.SaveUserSubscriptionPlan(objModel.UserId, objModel.PlanId, objModel.PlanAmount, objModel.PayAmount, objModel.PlanDiscount, objModel.Currency).FirstOrDefault();
        }

        public GetuserAuthorize_Result GetUserAuthorize(LoginModel loginModel)
        {
            var RESULT = Context.GetuserAuthorize(loginModel.Id, loginModel.UserName);
            return RESULT.FirstOrDefault();
        }

        public List<GetUserTemplates_Result> GetUserTemplates(UserTemplateModel userTemplateActiveModel)
        {
            return Context.GetUserTemplates(userTemplateActiveModel.UserId, userTemplateActiveModel.UserName).ToList();
        }

        public SaveUserTemplate_Result SaveUserTemplate(UserTemplateModel userTemplateModel)
        {
            return Context.SaveUserTemplate(userTemplateModel.TemplateId, userTemplateModel.UserId, userTemplateModel.Active).FirstOrDefault();
        }



    }

    public interface IUserRepositary : IGenericRepository<amazonchampEntities1>
    {
        RagisterUser_Result RegisterUser(UserModel userModel);
        LoginAuthenticate_Result LoginAuthenticte(LoginModel loginModel);
        GetuserAuthorize_Result GetUserAuthorize(LoginModel loginModel);
        SaveProduct_Result SaveProduct(UserProductModel user);
        IEnumerable<GetUserProducts_Result> GetUserProducts(UserProductModel user);
        ChangePassword_Result ChangePassword(ChangePasswordModel objModel);
        ForgetPassword_Result ForgetPassword(LoginModel objModel);
        IEnumerable<GetUserSubscriptionPlan_Result> GetUserSubscriptionPlan(UserSubscriptionPlanModel objModel);
        IEnumerable<GetSubscriptionPlan_Result> GetSubscriptionPlan(SubscriptionPlanModel objModel);
        SaveUserSubscriptionPlan_Result SaveUserSubscriptionPlan(SubscribeModel objModel);

        SaveUserTemplate_Result SaveUserTemplate(UserTemplateModel userTemplateActiveModel);
        List<GetUserTemplates_Result> GetUserTemplates(UserTemplateModel userTemplateActiveModel);
    }
}