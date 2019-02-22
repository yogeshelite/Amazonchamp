using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Repositary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Services
{
    public class KeywordResearchServices : IKeywordResearchServices
    {
        public IKeywordResearchRepositary _instance { get; set; }
        public KeywordResearchServices()
        {

        }
        public KeywordResearchServices(IKeywordResearchRepositary instance)
        {
            _instance = instance;
        }
        public ResponseModel GetKeyword(KeyWordResearchModel objKeyword)
        {
            var _result = _instance.GetKeywords(objKeyword);
            return new ResponseModel() { Response = JsonConvert.SerializeObject(_result), Success = true };
        }

        public ResponseModel SaveKeyword(KeyWordResearchModel objKeyword)
        {
            var _result = _instance.keywordResearch(objKeyword);
            return new ResponseModel() { Response = _result.Response, Success = _result.Success.Value };
        }
    }
    public interface IKeywordResearchServices
    {
        
        ResponseModel SaveKeyword(KeyWordResearchModel objKeyword);
        ResponseModel GetKeyword(KeyWordResearchModel objKeyword);
    }
    }