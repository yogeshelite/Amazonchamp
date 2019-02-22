using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime RecordTime { get; set; } = DateTime.Now;
        public string EmailId{ get; set; }
        public Guid Id { get; set; }

    }
}