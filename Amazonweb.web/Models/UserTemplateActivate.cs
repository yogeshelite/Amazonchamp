using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class UserTemplate
    {
        public long TemplateId { get; set; }
        public Guid? UserId { get; set; }
        public long Id { get; set; }
        public bool Active { get; set; }
        public string UserName { get;  set; }
        public string TemplateName { get;  set; }
    }
}