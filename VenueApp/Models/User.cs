using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.Models
{
    public class User
    {
        public int ID { get; set; }
        //[Index("UsernameIndex", IsUnique = true)]
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public DateTime Created { get; set; }  
        public UserType Type { get; set; }
        public int TypeID { get; set; }
        public Membership Membership { get; set; }
        public int MembershipID { get; set; }

    }
}
