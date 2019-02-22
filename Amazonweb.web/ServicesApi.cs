using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Amazonweb
{
 
    public class ServicesApi
    {
        public static string ApiUrl => Properties.Settings.Default.ApiUrl;

        #region Api Response
        /// <summary>
        /// GetApiResponse 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static HttpWebResponse GetApiResponse(string ApiMethod, string method, string requestData, string contentType = "application/json")
        {
            try
            {
                // System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
               var _request = (HttpWebRequest)WebRequest.Create(string.Format(ApiUrl, ApiMethod));
                _request.Method = method;
                _request.ContentType = contentType;
                //string credentials = string.Format("{0}:{1}", _authInfo.UserName, _authInfo.Password);
                //CredentialCache mycache = new CredentialCache();
                //_request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
                if (requestData.Length > 0)
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes((new { Data= requestData}).ToString());
                    using (var stream = _request.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, requestData.Length);

                    }
                }
                return ((HttpWebResponse)_request.GetResponse());
            }
            catch (WebException ex)
            {
                return ((HttpWebResponse)ex.Response);
            }
        }
        #endregion


    }
}