using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class SignupViewModel
    {        
        [Required]
        [RegularExpression("^[a-zA-Z]{5,15}$", ErrorMessage = "User mut be between 5 and 15 alphabetic characters")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter an alphanumeric password")]
        [DataType(DataType.Password)]
        [RegularExpression("^[A-Za-z0-9]{6,20}$", ErrorMessage = "Password must be between 6 and 20 alphanumeric characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field must match your password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Verify Password")]
        public string Verify { get; set; }

        public SignupViewModel()
        {

        }

    }
}
