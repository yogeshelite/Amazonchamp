using Amazonweb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class ProductSaveModel
    {
        public String ASIN { get; set; }
        //public String ProductName { get; set; }
        //public String ProductId { get; set; }
        public Guid UserId { get; set; }
        public long categoryId { get; set; }
       
        public bool isFeatured { get; set; }
        public string Operation { get; set; }

    }
}