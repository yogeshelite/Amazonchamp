using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class UserProductModel
    {
        public Guid  UserId { get; set; }
        public Guid ProductId { get; set; }

        public string ASIN { get; set; }
        public Boolean isFeatured { get; set; }
        public long categoryId { get; set; }
        public string Operation { get; set; }
    }
}