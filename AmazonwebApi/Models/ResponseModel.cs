﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonwebApi.Models
{
    public class ResponseModel
      
    {
        public Guid? Id { get; set; }
        public bool Success { get; set; }
        public string Response { get; set; }
    }
}