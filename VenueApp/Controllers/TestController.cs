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


        public ActionResult SetVariable(string key, string value)
        {
            HttpContext.Session.SetString(key,value);
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
            return RedirectToAction("Profile", "User", new { userId = HttpContext.Session.GetInt32("UserID")});
        }

    }
}
