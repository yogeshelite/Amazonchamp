using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class KeywordResearchModel
    {
        public String Id { get; set; } = "0";
        public String KeyWord { get; set; } = "";
        public long ExactMatchSearchVolume { get; set; } = 0;
        public long BroadMatchSearchVolume { get; set; } = 0;
        public long CategoryId { get; set; } = 0;
        public long RecommendedGiveaway { get; set; } = 0;
        public long HSABid { get; set; } = 0;
        public long ExactPPCBid { get; set; } =0;
        public long BroadPPCBid { get; set; } = 0;
        public long EaseToRank { get; set; } = 0;
        public long RelevancyScore { get; set; } =0;
        public string Operation { get; set; } = "";
    }
}