using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueApp.Data;
using VenueApp.Models;
using VenueApp.ViewModels;

namespace VenueApp.Controllers
{
    public class UserController : Controller
    {
        private VenueAppDbContext context;

        public UserController(VenueAppDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //IList<User> users = context.Users.Include(c => c.Type).ToList();
            IList<User> users = context.Users.ToList();
            return View(users);
        }

                     
        public IActionResult Signup()
        {
            SignupViewModel userViewModel = new SignupViewModel();

            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                // Add the new cheese to my existing users
                User newUser = new User
                {
                    Username = userFromView.Username,
                    Password = userFromView.Password,
                    TypeID = 1,         // Default for "Regular user"
                    MembershipID = 1    // Default for "None"
                    //Created = DateTime.Now
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                return Redirect("/User");
            }
            return View(userFromView);
        }

    }
}