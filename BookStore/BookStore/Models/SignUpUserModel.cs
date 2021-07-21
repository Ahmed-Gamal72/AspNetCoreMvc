using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please Enter Your first name")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Your last name")]

        public string LasttName { get; set; }

        [Required(ErrorMessage ="Please Enter Your Email")]
        [Display(Name ="Email Address")]
        [EmailAddress(ErrorMessage ="Please Enter a valid email")]
        public string Email { get; set; }

       [Required(ErrorMessage = "Please Enter Strong Password")]
       [Display(Name = "Password")]
       [Compare("ConfirmPassword",ErrorMessage ="Password dosn't match ")]
       [DataType(DataType.Password)]
        public string Password { get; set; }

      [Required(ErrorMessage = "Please Confirm Your Password")]
      [Display(Name = "Confirm Password")]
      [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
    }
}
