using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace VenueApp.Controllers
{
    public class DashboardController : Controller
    {
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
            HttpContext.Session.SetString("EndDate", DateTime.Now.AddSeconds(7).ToString("yyyy-MM-ddTHH:mm:ss"));
            ViewBag.EndDate = HttpContext.Session.GetString("EndDate");

            return View(); 
        }

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
        
                
    }
}