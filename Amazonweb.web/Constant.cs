using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb
{
    public class Constant
    {
        public const string AppDomainName = "http://localhost:50552/"; //using Template Path 
        public const string ApiLogin = "User/AuthenticateUser";
        public const string ApiRegister = "User/Registeration";
        public const string ApiCategory = "Default/GetProductCategory";
        public const string ApiSaveProduct = "User/SaveProduct";
        public const string ApiGetProduct = "User/UserProducts";
        public const string ApiGetTemplates = "Template/GetTemplates";
        public const string ApiSaveUserTemplate = "User/SaveTemplate";
        public const string ApiGetUserTemplates = "User/GetTemplates";
        public const string ApiSaveAbout = "About/SaveAbout";
        public const string ApiGetAbout = "About/GetAbout";
        public const string ApiGetUserAbout = "About/GetUserAbout";
        public const string ApiGetkeyword = "User/GetKeyword";
        public const string ApiSaveKeyword = "User/Savekeywords";
        public const string ApiChangePassword = "User/ChangePassword";
        public const string ApiForgetPassword = "User/ForgetPassword";
        public const string ApiGetUserSubscriptionPlan = "User/GetUserSubscriptionPlan";
        public const string ApiGetSubscriptionPlan = "User/GetSubscriptionPlan";
        public const String ApiSaveUserSubscriptionPlan = "User/SaveUserSubscriptionPlan";
        public const String ApiGetUserAuthorize = "User/GetUserAuthorize";



    }
}