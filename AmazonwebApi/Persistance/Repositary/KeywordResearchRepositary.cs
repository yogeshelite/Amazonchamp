using AmazonwebApi.Models;
using AmazonwebApi.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Persistance.Repositary
{
    public class KeywordResearchRepositary : GenericRepository<amazonchampEntities1>, IKeywordResearchRepositary
    {
        public IEnumerable<GetKeywords_Result> GetKeywords(KeyWordResearchModel objKeyword)
        {
            return Context.GetKeywords(objKeyword.KeyWord,objKeyword.CategoryId).ToList();

        }

        public ProductKeywords_Result keywordResearch(KeyWordResearchModel objKeyword)
        {
            return Context.ProductKeywords(objKeyword.Id, objKeyword.KeyWord, objKeyword.ExactMatchSearchVolume, objKeyword.BroadMatchSearchVolume, objKeyword.CategoryId, objKeyword.RecommendedGiveaway, objKeyword.HSABid, objKeyword.ExactPPCBid, objKeyword.BroadPPCBid, objKeyword.EaseToRank, objKeyword.RelevancyScore, objKeyword.Operation).FirstOrDefault();

        }
    }
    public interface IKeywordResearchRepositary : IGenericRepository<amazonchampEntities1>
    {
        ProductKeywords_Result keywordResearch(KeyWordResearchModel objKeyword);
       
        IEnumerable<GetKeywords_Result> GetKeywords(KeyWordResearchModel objKeyword);
    }
}