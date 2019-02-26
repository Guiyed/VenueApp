using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VenueApp.Data;
using VenueApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenueApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IHostingEnvironment he;
        private VenueAppDbContext context;
        
        public TestController(VenueAppDbContext dbContext, IHostingEnvironment e)
        {
            context = dbContext;
            he = e;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            /* Not needed anymore
            ViewBag.Session = HttpContext.Session.TryGetValue("user",out byte[]value);
            ViewBag.UserInSession = HttpContext.Session.GetString("user");
            */

            return View();
        }

        public IActionResult TBI()
        {
            return View();
        }


        
    }
}
