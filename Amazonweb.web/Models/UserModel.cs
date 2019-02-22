using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Amazonweb.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Enter Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter User Name")]
        [StringLength(maximumLength: 15, ErrorMessage = "username should 15 character long")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Password")]
        [StringLength(maximumLength:15,ErrorMessage ="Passwsord should be in 3 to 15 character long", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
        public DateTime RegisterationDate { get; set; } = DateTime.Now;

        

    }
}