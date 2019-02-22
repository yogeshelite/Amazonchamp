using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
      
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public DateTime RegisterationDate { get; set; } = DateTime.Now;
       

    }
}