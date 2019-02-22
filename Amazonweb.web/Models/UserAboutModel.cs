using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amazonweb.Models
{
    public class UserAboutModel
    {
        public long Id { get; set; }
        public String AboutTitle { get; set; }
        [AllowHtml]
        public String AboutSummary { get; set; }
        public Guid UserId { get; set; }
        public String PhoneNo { get; set; }
        public String Address { get; set; }
        public String Twitter { get; set; }
        public String Linkedin { get; set; }
        public String Facebook { get; set; }
        public String LogoImagePath { get; set; }
        public String AttachmentLogoName { get; set; }
    }
}