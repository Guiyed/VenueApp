using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.Models
{
    public class Booking
    {
        public int UserID { get; set; }
        public User User { get; set; }

        public int EventID { get; set; }
        public Event Event { get; set; }

        IList<Booking> Bookings { get; set; }


    }
}
