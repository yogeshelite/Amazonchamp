using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class GetActiveTemplateModel
    {
        public long Id { get; set; }
        public long TemplateId { get; set; }
        public Guid UserId { get; set; }
        public String TemplateName { get; set; }
        public DateTime ActivateDate { get; set; }
    }
}