using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Services
{
    public class ProductServices : IProductServices
    {
        public IProductRepositary _instance { get; set; }
        public ProductServices()
        {

        }
        public ProductServices(IProductRepositary instance)
        {
            _instance = instance;
        }
        public ResponseModel GetProductCategory()
        {
            var _result = _instance.GetCategory();
            return new ResponseModel() { Response = new JwtTokenManager().GenerateToken(JsonConvert.SerializeObject(_result)), Success =true};
        }
    }
    public interface IProductServices
    {
        ResponseModel GetProductCategory();
    }
}