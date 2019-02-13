using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenueApp.Models;

namespace VenueApp.ViewModels
{
    public class EditUserViewModel : AddUserViewModel
    {
        [Required]
        public int UserID { get; set; }

        public EditUserViewModel() { }

        public EditUserViewModel(IEnumerable<Membership> memberships, IEnumerable<UserType> roles) : base(memberships, roles)
        {
            
        }

        


    }


}

