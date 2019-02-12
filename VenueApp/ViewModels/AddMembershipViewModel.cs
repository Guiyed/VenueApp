using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class AddMembershipViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z]{1,12}$", ErrorMessage = "Membership must be between 1 and 12 alphabetic characters with no spaces")]
        [Display(Name = "Membership Name")]
        public string Name { get; set; }
        

    }
}
