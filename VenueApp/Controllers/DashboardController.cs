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

            //Error go Back to Login
            return RedirectToAction("Login", "User");
        }

        [Route("UserDashboard")]
        //[ChildActionOnly]
        public IActionResult UserDashboard ()
        {
            return View("User");
        }

        [Route("AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            return View("Admin");
        }

        
                
    }
}