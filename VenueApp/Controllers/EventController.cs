using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueApp.Data;
using VenueApp.Models;
using VenueApp.ViewModels;
using VenueApp.Helpers;

namespace VenueApp.Controllers
{
    public class EventController : Controller
    {
        private VenueAppDbContext context;

        public EventController(VenueAppDbContext dbContext)
        {
            context = dbContext;
        }



        //-------------------------------- INDEX -----------------------------------//
        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Event> events = context.Events.Include(c => c.Category).ToList();

            return View(events);
        }



        //-------------------------------- ADD EVENT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel(context.Categories.ToList());
            
            return View(addEventViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory newEventCategory =
                    context.Categories.Single(c => c.ID == addEventViewModel.CategoryID);

                // Add the new event to my existing events
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    Category = newEventCategory,
                    Price = addEventViewModel.Price,
                    Date = addEventViewModel.Date + addEventViewModel.Time,
                    Created = DateTime.Now
                };
                
                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Event");
            }

            addEventViewModel.SetCategories(context.Categories.ToList());
            return View(addEventViewModel);
        }



        //-------------------------------- REMOVE OR DELETE EVENT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Remove()
        {
            ViewBag.title = "Remove Events";
            ViewBag.events = context.Events.ToList();
            return View();
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Remove(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event theEvent = context.Events.Single(c => c.ID == eventId);
                context.Events.Remove(theEvent);
            }

            context.SaveChanges();

            return Redirect("/");
        }



        //-------------------------------- EDIT AN EVENT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Edit(int eventId)
        {
            Event eventToEdit = context.Events.Single(c => c.ID == eventId);
            EditEventViewModel editEventViewModel = new EditEventViewModel(context.Categories.ToList())
            {
                Name = eventToEdit.Name,
                Description = eventToEdit.Description,
                CategoryID = eventToEdit.CategoryID,
                EventID = eventToEdit.ID
            };

            return View(editEventViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Edit(EditEventViewModel modEvent)
        {
            if (ModelState.IsValid)
            {
                Event eventToEdit = context.Events.Single(c => c.ID == modEvent.EventID);

                EventCategory newEventCategory =
                    context.Categories.Single(c => c.ID == modEvent.CategoryID);

                // Modify the event with new information
                eventToEdit.Name = modEvent.Name;
                eventToEdit.Description = modEvent.Description;
                eventToEdit.Category = newEventCategory;
                                
                context.Events.Update(eventToEdit);
                context.SaveChanges();

                return Redirect("/");


            }
            return View(modEvent);

        }



        //----------------------------------- DETAILS -----------------------------------//
        // GET: /<controller>/
        public IActionResult Detail(int eventId)
        {

            Event selectedEvent = context.Events.Include(c => c.Category).Single(c => c.ID == eventId);

            Event eventToShow = new Event()
            {
                ID = selectedEvent.ID,
                Name = selectedEvent.Name,
                Description = selectedEvent.Description,
                Date = selectedEvent.Date,
                Price = selectedEvent.Price,
                Category = selectedEvent.Category,
                //Location = selectedEvent.Location??"",
                Created = selectedEvent.Created
            };

            return View(eventToShow);
        }





    }
}