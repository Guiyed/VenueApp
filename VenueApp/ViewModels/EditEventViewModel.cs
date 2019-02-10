using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenueApp.Models;

namespace VenueApp.ViewModels
{
    public class EditEventViewModel : AddEventViewModel
    {
        [Required]
        public int EventID { get; set; }

        public EditEventViewModel() { }

        public EditEventViewModel(IEnumerable<EventCategory> categories) : base(categories)
        {

        }
    }
}
