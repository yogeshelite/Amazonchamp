using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class AmazonProductModel
    {
        public String KeyWordId { get; set; }
        public String KeyWordName { get; set; }
        public String ExactMatchSearchVolume { get; set; }
        public String BroadMatchSearchVolume { get; set; }
        public String DominantCategory { get; set; }
        public String RecommendedGiveaway { get; set; }
        public String HSABid { get; set; }
        public String ExactPPCBid { get; set; }
        public String BroadPPCBid { get; set; }
        public String EaseToRank { get; set; }
        public String RelevancyScore { get; set; }
    }
}