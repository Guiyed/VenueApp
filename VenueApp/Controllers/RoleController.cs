using Microsoft.AspNetCore.Mvc;
using VenueApp.Models;
using System.Collections.Generic;
using VenueApp.ViewModels;
using VenueApp.Data;
using System.Linq;

namespace VenueApp.Controllers
{
    public class RoleController : Controller
    {
        private readonly VenueAppDbContext context;

        public RoleController(VenueAppDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<UserType> roles = context.Types.ToList();

            return View(roles);
        }              
    }
}