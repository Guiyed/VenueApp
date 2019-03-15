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
        public IActionResult Index(string activeUser)
        {
            //If the username is a Logged in user... get username else get empty ""
            ViewBag.Username = string.IsNullOrEmpty(activeUser as string) ? "" : activeUser;
            ViewBag.LogoutMessage = TempData["logoutMessage"] ?? "";
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            //IList<User> users = context.Users.ToList();   //Changing to Non "Deleted Users"
            //IList<User> users = context.Users.Where(c => c.Deleted == false).ToList();

            //Create empty list
            IList<User> users = new List<User>();

            //If there is an "Admin" Logged in the session
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //Add all the users to the list
                users = context.Users.Where(c => c.Deleted == false).ToList();
                return View(users);
            }
            //If there is an regular "User" Logged in the session
            else if (HttpContext.Session.GetString("Type") == "user")
            {
                //Show his details only
                /*return RedirectToAction("Detail", new { userId = HttpContext.Session.GetInt32("UserID") });
                       OR        */
                users = context.Users.Where(c => c.ID == HttpContext.Session.GetInt32("UserID")).ToList();
                return View(users);
            }
            //If not...
            else
            {
                //return empty list with Error Message
                ViewBag.ErrorMessage = "You need to be Logged In to access this feature";
                return View( users );
            }
        }



        //-------------------------------- LOGIN -----------------------------------//
        // GET: /<controller>/
        public IActionResult Login()
        {
            /* For future Refactoring */
            var currentSession = HttpContext.Session;
                       
            ViewBag.LogoutMessage = TempData["logoutMessage"] ?? "";
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            // If the user is already logged in
            if (currentSession.TryGetValue("User", out byte[] value))
            {
                //Go to Welcome Dashboard
                return RedirectToAction("Index", "Dashboard");
                //return RedirectToAction("Index", "User", new { username = HttpContext.Session.GetString("User") });
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
                    HttpContext.Session.SetInt32("UserID", currentUser.ID);
                    string userInSesion = HttpContext.Session.GetString("User");
                    string userType = context.Types.SingleOrDefault(c => c.ID == currentUser.TypeID).Name;
                    HttpContext.Session.SetString("Type", userType);
                    HttpContext.Session.SetString("ProfilePic", currentUser.ProfilePicture);                    

                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

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

            ViewBag.LogoutMessage = TempData["logoutMessage"] ?? "";
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            return View(userFromView);
        }



        //-------------------------------- LOGOUT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Logout()
        {
            //Delete or clear the Current Session
            HttpContext.Session.Clear();
            TempData["logoutMessage"] = "You have successfully logged out";

            //Return to Home Index verifiying there is no User in session
            return RedirectToAction("Index", "Home", new { username = HttpContext.Session.GetString("User") });
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
                    usernameAvaliable = true;
                }

                // If the Username is an Avaliable Username (It needs to be unique)
                if (usernameAvaliable)   
                {
                    // Add the new user to my existing users table
                    User newUser = new User
                    {
                        Username = userFromView.Username,
                        FirstName = userFromView.FirstName,
                        LastName = userFromView.LastName,
                        Email = userFromView.Email,
                        Password = userFromView.Password,
                        Created = DateTime.Now,
                        TypeID = 2,         // Default for "Regular user", needs to be implemented for the next database update
                        MembershipID = 1,    // Default for "None"
                                            //Created = DateTime.Now    //To be used when updating database, needs to be implemented for the next database update
                        ProfilePicture = "/images/Avatar3.svg"
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Create a new login session (Session["user"] = newUser.Username)
                    HttpContext.Session.SetString("User", newUser.Username);
                    HttpContext.Session.SetInt32("UserID", newUser.ID);
                    string userInSesion = HttpContext.Session.GetString("User");
                    string userType = context.Types.SingleOrDefault(c => c.ID == newUser.TypeID).Name;
                    HttpContext.Session.SetString("Type", newUser.Type.Name);
                    HttpContext.Session.SetString("ProfilePic", newUser.ProfilePicture);

                    TestFunctions.PrintConsoleMessage("LOGIN SUCCESS " + userInSesion);

                    // Greet the new user and redirect to its dashboard
                    return RedirectToAction("Index", "Dashboard");
                    //return RedirectToAction("Index", "User", new { username = userInSesion });
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


               
        //----------------------------------- ADD USER -----------------------------------//
        // GET: /<controller>/
        public IActionResult Add()
        {
            //If a Logged In "Admin" is accessing this feature
            if (HttpContext.Session.GetString("Type") == "admin"){
                //Display Add Form View
                AddUserViewModel userViewModel = new AddUserViewModel(context.Memberships.ToList(), context.Types.ToList());
                return View(userViewModel);
            }
            else
            {
                //Return Error. Only Admins can add users to database
                TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User")});
            }
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Add(AddUserViewModel userFromView)
        {
            if (ModelState.IsValid)
            {
                if (context.Users.SingleOrDefault(c => c.Username == userFromView.Username) == null)   // If is an Avaliable Username (It needs to be unique)
                {
                    // Add the new user to my existing users table
                    User newUser = new User
                    {
                        Username = userFromView.Username,
                        FirstName = userFromView.FirstName,
                        LastName = userFromView.LastName,
                        Email = userFromView.Email,
                        Password = userFromView.Password,
                        Created = DateTime.Now,
                        TypeID = userFromView.UserTypeID,
                        MembershipID = userFromView.MembershipID
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    // Success!!! user added...  return custom message
                    TempData["Message"] = "User " + newUser.Username + " was successfully created.";
                    TestFunctions.PrintConsoleMessage("SUCCESS, USER ADDED / CREATED");

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    // Cannot use this Username because there is already an User with this Username
                    ModelState.AddModelError("ServerError", "Sorry, but seems like someone else already has that Username. Please try with a different one.");
                    userFromView.ServerError = true;
                    TestFunctions.PrintConsoleMessage("DUPLICATED USER");
                }
            }

            userFromView.SetSelectList(context.Memberships.ToList(), context.Types.ToList());
            return View(userFromView);
        }



        //----------------------------------- DETAILS -----------------------------------//
        // GET: /<controller>/
        public IActionResult Detail(int userId)
        {
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            //If there is an "Admin" Logged in the session
            if (HttpContext.Session.GetString("Type") == "admin") {
                // Do nothing... The admin can view any userID
            }
            //If there is an regular "User" Logged in the session
            else if (HttpContext.Session.GetString("Type") == "user") {
                // Use the Logged user ID only
                int sessionID = HttpContext.Session.GetInt32("UserID").Value;
                if (userId != sessionID)
                {
                    ViewBag.ErrorMessage = "Sorry, but you can only access your User Details";
                }
                userId = sessionID;
            }
            else {
                //Return Error. Only Admins can add users to database
                TempData["ErrorMessage"] = "You need to be Logged In to access this feature";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });

            }

            // Select the user with the argument ID
            User selectedUser = context.Users.Include(c => c.Type).Include(d => d.Membership).SingleOrDefault(c => c.ID == userId);

            //If User exist in the database
            if (selectedUser != null)
            {
                //Create Model of user to show
                User userToShow = new User()
                {
                    ID = userId,
                    Username = selectedUser.Username,
                    FirstName = selectedUser.FirstName,
                    LastName = selectedUser.LastName,
                    Email = (selectedUser.Email ?? "-"),
                    Created = selectedUser.Created,
                    Membership = selectedUser.Membership,
                    Type = selectedUser.Type
                };

                //Render the HTML View with th complete users info
                return View(userToShow);
            }
            else
            {
                // The user does not exist in the Database and cannot be displayed... return custom message
                ViewBag.ErrorMessage = "The user you are triying to see, does not exist in the database";
                ViewBag.UserID = userId;
                return View(selectedUser);
            }
        }
    


        //-------------------------------- REMOVE -----------------------------------//
        // GET: /<controller>/
        public IActionResult Remove()
        {
            //If a Logged In "Admin" is accessing this feature
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //pass Non "deleted" and Non "Protected" users to view
                ViewBag.users = context.Users.Where(c => (c.Deleted == false) && (c.Protected == false)).ToList();
                return View();
            }
            else
            {
                //Return Error. Only Admins can delete/remove users to database
                TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });
            }
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Remove(int[] usersIds)
        {
            foreach (int userId in usersIds)
            {
                User theUser = context.Users.SingleOrDefault(c => c.ID == userId);
                if (theUser != null)
                {
                    theUser.Deleted = true;
                }
            }

            context.SaveChanges();

            // Success!!! user deleted...  return custom message
            TempData["Message"] = "User(s) successfully deleted.";
            TestFunctions.PrintConsoleMessage("SUCCESS, USER DELETED");

            return Redirect("/User");
        }




        //-------------------------------- EDIT AN USER -----------------------------------//
        // GET: /<controller>/
        public IActionResult Edit(int userId)
        {
            //If a Logged In "Admin" is accessing this feature
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //Select the User to delete or remove
                User userToEdit = context.Users.Where(c => c.Protected == false).SingleOrDefault(c => c.ID == userId);

                if (userToEdit != null)
                {
                    EditUserViewModel editUserViewModel = new EditUserViewModel(context.Memberships.ToList(), context.Types.ToList())
                    {
                        UserID = userToEdit.ID,
                        Username = userToEdit.Username,
                        FirstName = userToEdit.FirstName,
                        LastName = userToEdit.LastName,
                        Email = userToEdit.Email,
                        Password = userToEdit.Password,
                        UserTypeID = userToEdit.TypeID,
                        MembershipID = userToEdit.MembershipID,
                        Birthday = userToEdit.Birthday,
                        PhoneNumber = userToEdit.PhoneNumber,
                        Location = userToEdit.Location
                    };
                    return View(editUserViewModel);
                }
                else
                {
                    // The user does not exist in the Database or it is a protected user which cannot be updted... return custom message
                    TempData["ErrorMessage"] = "Sorry, The user does not exist in the Database or it is a protected user which cannot be updated.";
                    TestFunctions.PrintConsoleMessage("COULD NOT FIND THE USER IN THE DATABASE OR THE EVENT IS A PROTECTED ONE");
                }
                return Redirect("/User");
            }
            else
            {
                //Return Error. Only Admins can edit users inside the database
                TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });
            }
        }

        // PUT: /<controller>/  ?????????????????????????????????????????????????????????
        [HttpPut]
        [HttpPost]
        public IActionResult Edit(EditUserViewModel modUser)
        {
            if (ModelState.IsValid)
            {
                User userToEdit = context.Users.Where(c => c.Protected == false).SingleOrDefault(c => c.ID == modUser.UserID);

                if (userToEdit != null)
                {
                    Membership newUserMembership = context.Memberships.Single(c => c.ID == modUser.MembershipID);
                    UserType newUserRole = context.Types.Single(c => c.ID == modUser.UserTypeID);

                    // Modify the user with new information
                    userToEdit.Username = modUser.Username;
                    userToEdit.FirstName = modUser.FirstName;
                    userToEdit.LastName = modUser.LastName;
                    userToEdit.Membership = newUserMembership;
                    userToEdit.Type = newUserRole;
                    userToEdit.Email = modUser.Email;
                    userToEdit.Password = modUser.Password;
                    userToEdit.Birthday = modUser.Birthday;
                    userToEdit.PhoneNumber = modUser.PhoneNumber;
                    userToEdit.Location = modUser.Location;


                    context.Users.Update(userToEdit);
                    context.SaveChanges();

                    // Success!!! user updated...  return custom message
                    TempData["Message"] = "Great!... the user was successfully updated.";
                    TestFunctions.PrintConsoleMessage("SUCCESS, EVENT UPDATED CORRECTLY");


                    return Redirect("/User");
                }
                else
                {
                    // The user does not exist in the Database or it is a protected user which cannot be updted... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, The user does not exist in the Database or it is a protected user which cannot be updated.");
                    modUser.ServerError = true;
                    TestFunctions.PrintConsoleMessage("COULD NOT FIND THE USER IN THE DATABASE OR THE USER IS A PROTECTED ONE");
                }

            }
            modUser.SetSelectList(context.Memberships.ToList(), context.Types.ToList());
            return View(modUser);
        }



        //-------------------------------- Profile -----------------------------------//
        // GET: /<controller>/
        public IActionResult Profile(int userId = 1)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";


            // Check for User or Admin Role
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                // If user is Login but userId different from session.... Send error and display Logged User Profile
                if (HttpContext.Session.GetString("Type") == "user" && HttpContext.Session.GetInt32("UserID") != userId)
                {
                    ViewBag.ErrorMessage = "You cannot access another's user Profile";
                    userId = HttpContext.Session.GetInt32("UserID").GetValueOrDefault();
                }

                User selectedUser = context.Users.Where(c => c.Deleted == false).Include(c => c.Type).Include(d => d.Membership).SingleOrDefault(c => c.ID == userId);

                List<Booking> scheduledEvents = context
                                    .Bookings
                                    .Include(item => item.Event)
                                    .Include(c => c.Event.Category)
                                    .Where(cm => cm.UserID == userId)
                                    .ToList();

                if (selectedUser != null)
                {
                    ProfileViewModel userToShow = new ProfileViewModel(context.Memberships.ToList(), context.Types.ToList())
                    {
                        UserID = userId,
                        Username = selectedUser.Username,
                        FirstName = selectedUser.FirstName,
                        LastName = selectedUser.LastName,
                        Email = selectedUser.Email,
                        Password = selectedUser.Password,
                        Created = selectedUser.Created,
                        Membership = selectedUser.Membership,
                        Type = selectedUser.Type,
                        UserTypeID = selectedUser.TypeID,
                        MembershipID = selectedUser.MembershipID,
                        Birthday = selectedUser.Birthday,
                        PhoneNumber = selectedUser.PhoneNumber,
                        Location = selectedUser.Location,
                        Bookings = scheduledEvents

                    };

                    ViewBag.ProfileInfo = new Dictionary<string, string>()
                    {
                        {"Membership", userToShow.Membership.Name },
                        {"FullName", (userToShow.FirstName ?? (userToShow.LastName?? "-")) + " " + userToShow.LastName?? "" },
                        {"Email", userToShow.Email ?? "-" }
                    };

                    //Send all info to Profile View
                    return View(userToShow);
                }
            }

            //If user not logged in... Send Unauthorized Access page
            //TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
            return RedirectToAction("Index", "Dashboard");
        }
        
        // PUT: /<controller>/  ?????????????????????????????????????????????????????????
        [HttpPut]
        [HttpPost]
        public IActionResult Profile(EditUserViewModel modUser)
        {
            User userToEdit = context.Users.Include(c => c.Type).Include(d => d.Membership).SingleOrDefault(c => c.ID == modUser.UserID);

            ViewBag.ProfileInfo = new Dictionary<string, string>()
            {
                {"Membership", userToEdit.Membership.Name },
                {"FullName", (userToEdit.FirstName ?? (userToEdit.LastName?? "-")) + " " + userToEdit.LastName?? ""},
                {"Email", userToEdit.Email ?? "-" }
            };

            if (ModelState.IsValid)
            {
                if (userToEdit != null)
                {
                    // Modify the user with new information
                    userToEdit.Username = modUser.Username;
                    userToEdit.FirstName = modUser.FirstName;
                    userToEdit.LastName = modUser.LastName;
                    userToEdit.Email = modUser.Email;
                    userToEdit.Password = modUser.Password;
                    userToEdit.Birthday = modUser.Birthday;
                    userToEdit.PhoneNumber = modUser.PhoneNumber;
                    userToEdit.Location = modUser.Location;

                    context.Users.Update(userToEdit);
                    context.SaveChanges();
                }

                return RedirectToAction("Profile", new { userId = modUser.UserID });
            }

            ViewBag.Retry = true;
            return View(modUser);
        }
    }

}
