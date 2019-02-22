using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Services
{
   
    public class AboutServices: IAboutServices
    {
        public IAboutRepositary _instance { get; set; }
        public AboutServices()
        {

        }
        public AboutServices(IAboutRepositary instance)
        {
            _instance = instance;
        }

        public ResponseModel SaveAbout(AboutModel aboutModel)
        {
            var _result = _instance.SaveAbout(aboutModel);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }

        public ResponseModel GetAbout()
        {
            var _result = _instance.GetAbout();
            return new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(_result)), Success = true };
        }

        public ResponseModel GetUserAbout(AboutModel aboutModel)
        {
            var _result = _instance.GetUserAbout(aboutModel);
            return new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(_result)), Success = true };
        }
    }
    public interface IAboutServices
    {
        ResponseModel SaveAbout(AboutModel aboutModel);
       ResponseModel GetAbout();
        ResponseModel GetUserAbout(AboutModel aboutModel);
    }
}