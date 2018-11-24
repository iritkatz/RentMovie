using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models.ViewModels
{
    public class UserRegistrationViewModel
    {


        [Required]
        [EmailAddress]
        [Display(Name = " דוא\"ל ")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = " שם משתמש ")]
        public string username { get; set; }
        /*
                [Required]
                [MinLength(6 , ErrorMessage = "{0} חייבת להיות לפחות 6 תווים")]
                [Display(Name = "סיסמא")]
                public String Password { get; set; }

                [Required]
                [Compare("Password")]
                [Display(Name = "אימות סיסמא")]
                public String ConfirmPassword { get; set; }

               */



    }
}