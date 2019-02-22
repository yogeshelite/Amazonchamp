using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class UserSubscriptionPlanModel
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public long PlanId { get; set; }
        public double PlanAmount { get; set; }
        public double PayAmount { get; set; }
        public double Discount { get; set; }
        public double Balance { get; set; }
        public string Currency { get; set; }
        public string PendingDay { get; set; }
        public String PanelExpire { get; set; }
    }
}