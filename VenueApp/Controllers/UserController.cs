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
using VenueApp.Helpers;

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
        public IActionResult Index(string username)
        {
            //If the username is a Login user get username else get empty ""
            ViewBag.Username = string.IsNullOrEmpty(username as string) ? "" : username;
            ViewBag.LogoutMessage = TempData["logoutMessage"] ?? "";
            //IList<User> users = context.Users.Include(c => c.Type).ToList();
            IList<User> users = context.Users.ToList();
            return View(users);
        }



        //-------------------------------- LOGIN -----------------------------------//
        // GET: /<controller>/
        public IActionResult Login()
        {
            /* For future Refactoring
            var currentSession = HttpContext.Session;
            currentSession.TryGetValue("user", out byte[] value1);
            */

            // If the user is already logged in
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                //return RedirectToAction("Index", "User", new { username = HttpContext.Session.GetString("User") });
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                LoginViewModel userViewModel = new LoginViewModel();
                return View(userViewModel);
            }
            
        }

        // POST: /<controller>/

        [HttpPost]
        public IActionResult Login(LoginViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                User currentUser = context.Users.SingleOrDefault(c => c.Username == userFromView.Username);
                
                if ((currentUser != null) && (currentUser.Password == userFromView.Password))
                {
                    //Login Success... Greet the User
                    HttpContext.Session.SetString("User", currentUser.Username);
                    string userInSesion = HttpContext.Session.GetString("User");
                    string userType = context.Types.SingleOrDefault(c => c.ID == currentUser.TypeID).Name;
                    HttpContext.Session.SetString("Type", userType);
                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

                    //return RedirectToAction("Index", "User", new { username = userInSesion });
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (currentUser == null)
                {
                    // User Does not exist in the database... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, we couldn't find an account with that Username");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("USER DOES NOT EXIST IN THE DATABASE");
                }
                else
                {
                    // Password Does not Match with the one in the database... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, that password isn't right.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("PASSWORD DOES NOT MATCH BETWEEN THE FORM AND DATABASE");
                }
            }
            return View(userFromView);
        }



        //-------------------------------- LOGOUT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Logout()
        {
            //Delete or clear the Current Session
            HttpContext.Session.Clear();
            TempData["logoutMessage"] = "You have successfully logged out";

            return RedirectToAction("Index", "User", new { username = HttpContext.Session.GetString("User") });
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
            bool usernameAvaliable = false;

            if (ModelState.IsValid)
            {
                /*
                //............. Should I be doing this here or in the view model, Front End??? Wheere ......??????????????
                
                //User existingUser = context.Users.Find(userFromView.Username);
                //existingUser = context.Users.SingleOrDefault(c => c.Username == userFromView.Username);
                */
              
                try
                {
                    //Check for the availability of the selected username on the database 
                    context.Users.Single(c => c.Username == userFromView.Username);
                }
                catch
                {
                    //The username does not exist in the database
                    usernameAvaliable =true;
                }
                
                if (usernameAvaliable)   // If is an Avaliable Username (It needs to be unique)
                {
                    // Add the new user to my existing users table
                    User newUser = new User
                    {
                        Username = userFromView.Username,
                        FirstName = userFromView.FirstName,
                        LastName = userFromView.LastName,
                        Email = userFromView.Email,
                        Password = userFromView.Password,
                        TypeID = 2,         // Default for "Regular user", needs to be implemented for the next database update
                        MembershipID = 1    // Default for "None"
                                            //Created = DateTime.Now    //To be used when updating database, needs to be implemented for the next database update
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Create a new login session (Session["user"] = newUser.Username)
                    HttpContext.Session.SetString("User", newUser.Username);
                    string userInSesion = HttpContext.Session.GetString("User");
                    string userType = context.Types.SingleOrDefault(c => c.ID == newUser.TypeID).Name;
                    HttpContext.Session.SetString("Type", newUser.Type.Name);
                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

                    // Greet the new user and redirect to its dashboard
                    return RedirectToAction("Index", "User", new { username = userInSesion });
                }
                else
                {
                    // Cannot use this Username because there is already an User with this Username
                    ModelState.AddModelError("ServerError", "Sorry, but seems like someone else already has that Username. Please try with a different one.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("DUPLICATED USER");
                }
            }
            return View(userFromView);
        }
    }
}