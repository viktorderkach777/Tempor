using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class SignupBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Timezone")]
        public string Timezone { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirmation { get; set; }
    }
    public class SignupResultModel
    {
        public ErrorsSignupBindingModel errors { get; set; }
        public bool isValid { get; set; }
    }

    public class ErrorsSignupBindingModel
    {
        public string email { get; set; }

        public string username { get; set; }

        public string timezone { get; set; }

        public string password { get; set; }

        public string passwordConfirmation { get; set; }
    }
    public class ErrorsLoginModel
    {
        public string identifier { get; set; }

        public string password { get; set; }
    }
}