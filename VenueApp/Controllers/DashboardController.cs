using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.IO;
using VenueApp.Data;
using VenueApp.Models;



namespace VenueApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly string TicketmasterAPIkey = "rjvAOXYLhx1XPB30QYsgr5QVhQVO3U4b";
        private readonly string BingMapsAPIkey = "Ar6GSgDklc17CZg1iXfmAutlA2Kru2EpLP0NFvJmllNtv3QX2VTgP3YBSY2AVVUu";



        private readonly IHostingEnvironment he;
        private VenueAppDbContext context;

        public DashboardController(VenueAppDbContext dbContext, IHostingEnvironment e)
        {
            context = dbContext;
            he = e;
        }



        //-------------------------------- INDEX -----------------------------------//
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                string userType = HttpContext.Session.GetString("Type");

                if (userType == "user")
                {
                    return Redirect("/UserDashboard");
                }
                else if (userType == "admin")
                {
                    return Redirect("/AdminDashboard");
                }
            }
            
            //Unauthorize Access - Give Error and go Back to Login
            ViewBag.UnauthorizedMessage = new string[] { "Sorry you are not authorized to access this feature, " +
                "please Log In first.",
                "You are being redirected to the User Login Page in a few seconds." };

            //Set Timer before Redirection (AddSeconds to timer before the countdown) 
            HttpContext.Session.SetString("EndDate", DateTime.Now.AddSeconds(3).ToString("yyyy-MM-ddTHH:mm:ss"));
            ViewBag.EndDate = HttpContext.Session.GetString("EndDate");

            return View(); 
        }



        //-------------------------------- USER DASHBOARD -----------------------------------//
        // GET: /<controller>/
        [Route("UserDashboard")]
        //[ChildActionOnly]
        public IActionResult UserDashboard ()
        {
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                return View("User");
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }         
        }


        //-------------------------------- ADMIN DASHBOARD -----------------------------------//
        // GET: /<controller>/
        [Route("AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                return View("Admin");
            }
            else
            {
                return RedirectToAction("Index", "Dashboard");
            }
        }



        //-------------------------------- PROFILE PICS -----------------------------------//
        // GET: /<controller>/
        public ActionResult SetVariable(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
            //Update USER database

            User userToEdit = context.Users.Include(c => c.Type).Include(d => d.Membership).SingleOrDefault(c => c.ID == HttpContext.Session.GetInt32("UserID"));
            userToEdit.ProfilePicture = HttpContext.Session.GetString("ProfilePic");
            context.Users.Update(userToEdit);
            context.SaveChanges();

            return this.Json(new { success = true });
        }
        
        [HttpPost]
        public ActionResult UploadProfilePic(IFormFile image)
        {

            if (image != null)
            {
                var fileName = Path.Combine(he.WebRootPath + "\\images\\profilepics\\", HttpContext.Session.GetString("User") + Path.GetExtension(image.FileName));
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                image.CopyTo(fs);
                HttpContext.Session.SetString("ProfilePic", "/images/profilepics/" + HttpContext.Session.GetString("User") + Path.GetExtension(image.FileName));
                fs.Close();

                User userToEdit = context.Users.Include(c => c.Type).Include(d => d.Membership).SingleOrDefault(c => c.ID == HttpContext.Session.GetInt32("UserID"));
                userToEdit.ProfilePicture = "/images/profilepics/" + HttpContext.Session.GetString("User") + Path.GetExtension(image.FileName);
                context.Users.Update(userToEdit);
                context.SaveChanges();
            }
            return RedirectToAction("Profile", "User", new { userId = HttpContext.Session.GetInt32("UserID") });
        }



        //-------------------------------- API -----------------------------------//
        // GET: /<controller>/
        public IActionResult API()
        {
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                string userType = HttpContext.Session.GetString("Type");

                if (userType == "admin")
                {
                    ViewBag.APIkey = TicketmasterAPIkey;
                    return View();
                }
            }

            //Unauthorize Access - Give Error and go Back to Dashboard
            ViewBag.UnauthorizedMessage = new string[] { "Sorry you are not authorized to access this feature, " +
                "You are being redirected to the User Login Page in a few seconds." };

            return RedirectToAction("Index", "Dashboard");
        }




    }
}