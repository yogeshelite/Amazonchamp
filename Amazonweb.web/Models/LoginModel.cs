using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class LoginModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Enter UserName/Email")]
        public string UserName { get; set; }
      
        [Required(ErrorMessage = "Enter Password")]
        [StringLength(maximumLength:15,ErrorMessage ="Passwsord should be in 3 to 15 character long", MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
      
      
    }
}