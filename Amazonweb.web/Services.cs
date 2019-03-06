using Amazonweb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Amazonweb
{
 
    public class Services
    {
        public static string ApiUrl =>string.Format("{0}/{1}",(ConfigurationManager.AppSettings["IsTest"].Equals("1") ) ? "http://localhost:49938" : "http://192.168.1.175:2200",  Properties.Settings.Default.ApiUrl);
        #region Api Response

        public static HttpWebResponse GetApiResponse(string url, string metthod, string postData)
        {
            try
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(string.Concat("{Data:\"", postData, "\"}"));
              //  string encoded = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Properties.Settings.Default.DegaAPIUser + ":" + Properties.Settings.Default.DegaAPIPassword));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format (ApiUrl,url)));
                request.Method = metthod;
                request.ContentType = "application/json";
                request.ContentLength = data.Length;
                
                
             //   request.Headers.Add("Authorization", "Basic " + encoded);


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    return ((HttpWebResponse)request.GetResponse());
                }
            }
            catch (WebException ex)
            {
                return ((HttpWebResponse)ex.Response);
            }
        }

        internal static object GetCookie(object httpContext, string v)
        {
            throw new NotImplementedException();
        }

        public static void SetCookie(HttpContextBase httpContext, string name,string value)
        {
            HttpCookie cookie = new HttpCookie(name, value);
            httpContext.Response.Cookies.Add(cookie);

        }
        public static void RemoveCookie(HttpContextBase httpContext,string name)
        {
            //System.Web.HttpContext.Response.Cookies.Remove(cookieName); // for example .ASPXAUTH
            HttpCookie cookie = new HttpCookie(name);
            cookie.Expires= DateTime.Now.AddDays(-1);
            httpContext.Response.Cookies.Add(cookie);

        }
        public static HttpCookie GetCookie(HttpContextBase httpContext, string name)
        {
            if (httpContext.Request.Cookies.AllKeys.Contains(name)) return httpContext.Request.Cookies[name];
            return null;


        }

        public static UserModel GetLoginUser(HttpContextBase httpContext, JwtTokenManager jwtTokenManager)
        { UserModel login=null;
             var cookiesValue = Services.GetCookie(httpContext, "usr");
            if(cookiesValue==null) return login;
            dynamic _data = jwtTokenManager.DecodeToken(cookiesValue.Value);
            var json = JsonConvert.DeserializeObject<Dictionary<string, object>>(_data);
            if (json.ContainsKey("unique_name"))
            {
                login = JsonConvert.DeserializeObject<UserModel>(json["unique_name"].ToString());
             
            }
            return login;
        }
      
        #endregion


    }
}