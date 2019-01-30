using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
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


        //-------------------------------- LOGIN -----------------------------------//
        // GET: /<controller>/
        public IActionResult Login()
        {
            LoginViewModel userViewModel = new LoginViewModel();

            return View(userViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Login(LoginViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                User currentUser;
                try {
                    currentUser = context.Users.Single(c => c.Username == userFromView.Username);
                }
                catch
                {
                    currentUser = null;
                }

                //User testuser = context.Users.Find(userFromView.Username);

                if ((currentUser != null) && (currentUser.Password == userFromView.Password))
                {
                    HttpContext.Session.SetString("user", currentUser.Username);

                    //Login Success
                    Console.WriteLine("\n");
                    Console.WriteLine("------------------Loggin Succcess------------------");
                    string sesion = HttpContext.Session.GetString("user");
                    Console.WriteLine(sesion);
                    Console.WriteLine("\n");

                    return Redirect("/User");
                }
                else if (currentUser == null)
                {
                    // User Does not exist in the database... return custom message
                    Console.WriteLine("\n");
                    Console.WriteLine("------------------User Does Not exist------------------");
                    Console.WriteLine("\n");
                }
                else
                {
                    // Password Does not Match with stored one in the database... return custom message
                    Console.WriteLine("\n");
                    Console.WriteLine("--------------Password Does not match------------------");
                    Console.WriteLine("\n");
                }

            }

            return View(userFromView);

        }


        //----------------------------------- SIGNUP -----------------------------------//
                // GET: /<controller>/
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

                //Session["user"] = newUser.Username;
                HttpContext.Session.SetString("user", newUser.Username);

                Console.WriteLine("/n");
                Console.WriteLine(HttpContext.Session.GetString("user"));
                Console.WriteLine("/n");

                return Redirect("/User");
            }
            return View(userFromView);
        }

    }
}