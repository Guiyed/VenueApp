using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VenueApp.Models;

namespace VenueApp.ViewModels
{
    public class ViewScheduledViewModel
    {
        public User User { get; set; }
        public IList<Booking> Bookings { get; set; }

    }
}
