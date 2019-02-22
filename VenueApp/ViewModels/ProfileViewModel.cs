using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenueApp.Models;

namespace VenueApp.ViewModels
{
    public class ProfileViewModel : EditUserViewModel
    {
        [Required]
        public IList<Booking> Bookings { get; set; }



        public ProfileViewModel() { }

        public ProfileViewModel(IEnumerable<Membership> memberships, IEnumerable<UserType> roles) : base(memberships, roles)
        {
            
        }

        


    }


}

