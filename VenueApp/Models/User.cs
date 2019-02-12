using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VenueApp.Models
{
    public class User
    {
        public int ID { get; set; }
        //[Index("UsernameIndex", IsUnique = true)] // not suported on EF core --> Found a workaround in dbcontext.
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }  
        public UserType Type { get; set; }
        public int TypeID { get; set; }
        public Membership Membership { get; set; }
        public int MembershipID { get; set; }

        public bool Protected { get; set; }
        public bool Deleted { get; set; }

    }
}
