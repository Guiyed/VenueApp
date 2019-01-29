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
        [RegularExpression("^[a-zA-Z0-9]{6,20}$", ErrorMessage = "User must be between 6 and 20 alphanumeric characters")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]{1,}$", ErrorMessage = "Do not use numbers or special caracters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z]{1,}$", ErrorMessage = "Do not use numbers or special caracters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must enter an alphanumeric password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}", ErrorMessage = "Password must have at least one lowercase, one uppercase letter, one number and be at least six characters")]
        //[RegularExpression("^[A-Za-z0-9]{6,20}$", ErrorMessage = "Password must be between 6 and 20 alphanumeric characters")]
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
