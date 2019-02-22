using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class ResponseModel
    {
        public Guid? Id { get; set; }
        public string Response { get; set; }

       public bool Success { get; set; }
        //public string orderBy { get; set; }
        //public bool ShowAll { get; internal set; }
    }
}