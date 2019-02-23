using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenueApp.Controllers
{
    public class TestController : Controller
    {
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

    }
}
