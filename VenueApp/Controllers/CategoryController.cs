using Microsoft.AspNetCore.Mvc;
using VenueApp.Models;
using System.Collections.Generic;
using VenueApp.ViewModels;
using VenueApp.Data;
using System.Linq;


namespace VenueApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly VenueAppDbContext context;

        public CategoryController(VenueAppDbContext dbContext)
        {
            context = dbContext;
        }



        //-------------------------------- INDEX -----------------------------------//
        // GET: /<controller>/
        public IActionResult Index()
        {
            //List<EventCategory> categories = context.Categories.ToList();

            //pass Non "deleted" categories 
            List<EventCategory> categories = context.Categories.Where(c => c.Deleted == false).ToList();

            return View(categories);
        }



        //-------------------------------- ADD -----------------------------------//
        // GET: /<controller>/
        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                // Add the new category to my existing categories
                EventCategory newCategory = new EventCategory
                {
                    Name = addCategoryViewModel.Name
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/Category");
            }

            return View(addCategoryViewModel);
        }

        //-------------------------------- REMOVE -----------------------------------//
        // GET: /<controller>/
        public IActionResult Remove()
        {
            //ViewBag.categories = context.Categories.ToList();

            //pass Non "deleted" and Non Protected categories 
            ViewBag.categories = context.Categories.Where(c => (c.Deleted == false) && (c.Protected == false)).ToList();

            return View();
        }

        // GET: /<controller>/
        [HttpPost]
        public IActionResult Remove(int[] categoryIds)
        {
            foreach (int categoryId in categoryIds)
            {
                EventCategory theCategory = context.Categories.SingleOrDefault(c => c.ID == categoryId);
                //context.Categories.Remove(theCategory);       //Change to Deleted Flag to avoid loss of data

                // Deleted Flag method
                if (theCategory != null)
                {
                    theCategory.Deleted = true;
                }

            }

            context.SaveChanges();

            return Redirect("/Category");
        }


    }
}