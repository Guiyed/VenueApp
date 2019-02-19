using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenueApp.Models;

namespace VenueApp.ViewModels
{
    public class BookingViewModel
    {
        [Required]
        [Display(Name = "Event")]
        public int EventID { get; set; }
        [Required]
        public int UserID { get; set; }

        public User User { get; set; }
        public List<SelectListItem> Events { get; set; }

        public BookingViewModel() { }

        public BookingViewModel(User user, IEnumerable<Event> events)
        {
            User= user;
            Events = new List<SelectListItem>();

            foreach (Event evento in events)
            {
                Events.Add(new SelectListItem
                {
                    Value = evento.ID.ToString(),
                    Text = evento.Name
                });
            }

        }
    }
}
