using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class SubscribeModel
    {
        public Guid UserId { get; set; }
        public long PlanId { get; set; }
        public String PlanName { get; set; }
        public decimal PlanAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal PlanDiscount { get; set; }
        public string Currency { get; set; }
        public string PendingDay { get; set; }
    }
}