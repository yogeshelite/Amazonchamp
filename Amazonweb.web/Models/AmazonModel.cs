using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmzonWebApi.Models
{
    public class AmazonModel
    {
        public string ASIN { get; set; }
        public string MerchantId { get; set; }
        public String CategoryNames { get; set; }
        public String MinimumPrice { get; set; }
        public string MaximumPrice { get; set; }
        public string SearchItemName { get; set; }
        public string Brand { get; set; }
        public string MinPercentageOff { get; set; }
        public string Title { get; set; }
    }
}