//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AmazonwebApi.Persistance.Data
{
    using System;
    
    public partial class GetUserSubscriptionPlan_Result
    {
        public long Id { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<long> PlanId { get; set; }
        public Nullable<decimal> PlanAmount { get; set; }
        public Nullable<decimal> PayAmount { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string Currency { get; set; }
        public Nullable<int> PendingDay { get; set; }
        public string PanelExpire { get; set; }
    }
}