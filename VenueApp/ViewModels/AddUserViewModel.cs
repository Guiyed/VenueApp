using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenueApp.Models;

namespace VenueApp.ViewModels
{
    public class AddUserViewModel : SignupViewModel
    {
        [Required]
        [Display(Name = "Membership")]
        public int MembershipID { get; set; }

        
        public List<SelectListItem> Memberships { get; set; }

        [Required]
        [Display(Name = "Role")]
        public int UserTypeID { get; set; }

        
        public List<SelectListItem> Roles { get; set; }


        public AddUserViewModel() { }

        public AddUserViewModel(IEnumerable<Membership> memberships, IEnumerable<UserType> roles)
        {
            SetSelectList(memberships, roles);
        }

        public void SetSelectList(IEnumerable<Membership> memberships, IEnumerable<UserType> roles)
        {
            Memberships = new List<SelectListItem>();
            Roles = new List<SelectListItem>();


            foreach (Membership level in memberships)
            {

                Memberships.Add(new SelectListItem
                {
                    Value = ((int)level.ID).ToString(),
                    Text = level.Name.ToString(),
                });

            }

            foreach (UserType role in roles)
            {

                Roles.Add(new SelectListItem
                {
                    Value = ((int)role.ID).ToString(),
                    Text = role.Name.ToString(),
                });

            }

        }
    }


}

