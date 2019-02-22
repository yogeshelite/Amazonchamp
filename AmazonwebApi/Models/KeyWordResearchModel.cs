using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class KeyWordResearchModel
    {
        public long Id { get; set; }
        public String KeyWord { get; set; }
        public long ExactMatchSearchVolume { get; set; }
        public long BroadMatchSearchVolume { get; set; }
        public long CategoryId { get; set; }
        public long RecommendedGiveaway { get; set; }
        public long HSABid { get; set; }
        public long ExactPPCBid { get; set; }
        public long BroadPPCBid { get; set; }
        public long EaseToRank { get; set; }
        public long RelevancyScore { get; set; }
        public String Operation { get; set; }

    }
}