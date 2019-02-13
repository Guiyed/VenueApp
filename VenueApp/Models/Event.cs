using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public EventCategory Category { get; set; }
        public int CategoryID { get; set; }
        //public String Location {get; set;}
        public DateTime Created { get; set; }     

        public bool Protected { get; set; }
        public bool Deleted { get; set; }


    }
}
