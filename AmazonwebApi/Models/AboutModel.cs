using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class AboutModel
    {
        public String AboutTitle { get; set; }
        public String AboutSummary { get; set; }
        public Guid UserId { get; set; }
        public string PhoneNo { get; set; }
        public String Address { get; set; }
        public String Twitter { get; set; }
        public String Linkedin { get; set; }
        public String Facebook { get; set; }
        public String LogoImagePath { get; set; }
        public String AttachmentLogoName { get; set; }
    }
}