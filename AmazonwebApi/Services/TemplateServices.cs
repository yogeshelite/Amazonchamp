using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Services
{
    public class TemplateServices : ITemplateServices
    {

        public ITemplateRepositary _instance { get; set; }

        public TemplateServices()
        {

        }
        public TemplateServices(ITemplateRepositary instance)
        {
            _instance = instance;
        }

       
        public ResponseModel GetTemplates()
        {
            var _result = _instance.GetTemplates();
            return new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(_result)), Success = true };
        }
    }
    public interface ITemplateServices
    {
        ResponseModel GetTemplates();
    }
}