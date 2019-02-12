using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.Models
{
    public class UserType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        
        public bool Protected { get; set; }
        public bool Deleted { get; set; }
    }
}
