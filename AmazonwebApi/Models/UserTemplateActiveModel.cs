using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class UserTemplateModel
    {
        public long TemplateId { get; set; }
        public Guid? UserId { get; set; }
        public long Id { get; set; }
        public bool  Active { get; set; }
        public string UserName { get; internal set; }
        public string TemplateName { get; internal set; }
    }
}