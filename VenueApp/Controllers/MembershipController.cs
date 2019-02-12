using Microsoft.AspNetCore.Mvc;
using VenueApp.Models;
using System.Collections.Generic;
using VenueApp.ViewModels;
using VenueApp.Data;
using System.Linq;

namespace VenueApp.Controllers
{
    public class MembershipController : Controller
    {
        private readonly VenueAppDbContext context;

        public MembershipController(VenueAppDbContext dbContext)
        {
            context = dbContext;
        }



        //-------------------------------- INDEX -----------------------------------//
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Membership> memberships = context.Memberships.ToList();

            return View(memberships);
        }



        //-------------------------------- ADD -----------------------------------//
        // GET: /<controller>/
        public IActionResult Add()
        {
            AddMembershipViewModel addMembershipViewModel = new AddMembershipViewModel();
            return View(addMembershipViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Add(AddMembershipViewModel addMembershipViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to my existing categories
                Membership newMembership = new Membership
                {
                    Name = addMembershipViewModel.Name
                };

                context.Memberships.Add(newMembership);
                context.SaveChanges();

                return Redirect("/Membership");
            }

            return View(addMembershipViewModel);
        }



        //-------------------------------- REMOVE -----------------------------------//
        // GET: /<controller>/
        public IActionResult Remove()
        {
            ViewBag.memberships = context.Memberships.ToList();
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Remove(int[] membershipIds)
        {
            foreach (int membershipId in membershipIds)
            {
                Membership theMembership = context.Memberships.Single(c => c.ID == membershipId);
                context.Memberships.Remove(theMembership);
            }

            context.SaveChanges();

            return Redirect("/Membership");
        }
    }
}