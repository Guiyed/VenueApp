using Microsoft.AspNetCore.Mvc;

namespace VenueApp.Helpers
{
    public class ViewbagManager : Controller
    {
        public ViewbagManager() { }

        //Methods
        public void SetMessages()
        {
            ViewBag.LogoutMessage = TempData["logoutMessage"] ?? "";
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
        }

    }
}
