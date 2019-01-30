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


        //-------------------------------- INDEX -----------------------------------//
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
                    ModelState.AddModelError("ServerError", "Sorry, we couldn't find an account with that Username");
                    Console.WriteLine("\n");
                    Console.WriteLine("------------------User Does Not exist------------------");
                    Console.WriteLine("\n");             
                }
                else
                {
                    // Password Does not Match with stored one in the database... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, that password isn't right.");
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

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Signup(SignupViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                //............. Should I be doing this here or in the view model, Front End??? Wheere ......??????????????
                
                //User existingUser = context.Users.Find(userFromView.Username);
                User existingUser;                
                try
                {
                    existingUser = context.Users.Single(c => c.Username == userFromView.Username);
                }
                catch
                {
                    existingUser = null;
                }

                //User existingUser = context.Users.Single(c => c.Username == userFromView.Username);
                if (existingUser == null)   // if Avaliable Username (I need to be unique)
                {
                    // Add the new user to my existing users table
                    User newUser = new User
                    {
                        Username = userFromView.Username,
                        FirstName = userFromView.FirstName,
                        LastName = userFromView.LastName,
                        Email = userFromView.Email,
                        Password = userFromView.Password,
                        TypeID = 1,         // Default for "Regular user", needs to be implemented for the next database update
                        MembershipID = 1    // Default for "None"
                                            //Created = DateTime.Now    //To be used when updating database, needs to be implemented for the next database update
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Create a new login session
                    //Session["user"] = newUser.Username;
                    HttpContext.Session.SetString("user", newUser.Username);

                    //TEST
                    Console.WriteLine("/n");
                    Console.WriteLine(HttpContext.Session.GetString("user"));
                    Console.WriteLine("/n");

                    // Greet the new user and redirect to its dashboard (to be updated)
                    return Redirect("/User");
                }
                else
                {
                    // Cannot use this Username because there is already an User with this Usename
                    ModelState.AddModelError("ServerError", "Sorry, but seems like someone else already has that Username. Please try with a different one.");
                    Console.WriteLine("\n");
                    Console.WriteLine("------------------User already taken. Please select a new one------------------");
                    Console.WriteLine("\n");

                    
                }
            }
                
            return View(userFromView);
        }

    }
}