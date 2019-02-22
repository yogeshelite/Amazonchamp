using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class UserProduct
    {
        public Guid Id { get; set; }

        public string ASIN { get; set; }
        public Boolean isFeatured { get; set; }
        public long CategoryId { get; set; }
       
    }
}