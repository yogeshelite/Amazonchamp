using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class SubscriptionPlanModel
    {
        public int PlanId { get; set; }
        public String PlanName { get; set; }
        public double PlanAmount { get; set; }
        public String Currency { get; set; }
    }
}