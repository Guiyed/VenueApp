using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenueApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IHostingEnvironment he;

        public TestController(IHostingEnvironment e)
        {
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

            return this.Json(new { success = true });
        }


        [HttpPost]
        public ActionResult UploadProfilePic(IFormFile image)
        {

            if (image != null)
            {
                
                //var fileName = Path.Combine(he.WebRootPath + "\\images\\",  Path.GetFileName(image.FileName));
                var fileName = Path.Combine(he.WebRootPath + "\\images\\", HttpContext.Session.GetString("User") + Path.GetExtension(image.FileName));
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                image.CopyTo(fs);
                //image.CopyTo(new FileStream(fileName, FileMode.OpenOrCreate));
                //image.CopyTo(new FileStream(fileName, FileMode.Create));
                HttpContext.Session.SetString("ProfilePic", "/images/"+ HttpContext.Session.GetString("User") + Path.GetExtension(image.FileName));
                fs.Close();
            }                       
            //Update USER database

            return RedirectToAction("Profile", "User", new { userId = HttpContext.Session.GetInt32("UserID")});
        }




    }
}
