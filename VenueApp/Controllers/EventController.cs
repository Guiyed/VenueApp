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
using System.IO;
using System.Runtime.Serialization.Json;

namespace VenueApp.Controllers
{
    public class EventController : Controller
    {
        private readonly string TicketmasterAPIkey = "rjvAOXYLhx1XPB30QYsgr5QVhQVO3U4b";
        private readonly string BingMapsAPIkey = "Ar6GSgDklc17CZg1iXfmAutlA2Kru2EpLP0NFvJmllNtv3QX2VTgP3YBSY2AVVUu";

        private VenueAppDbContext context;

        public EventController(VenueAppDbContext dbContext)
        {
            context = dbContext;
        }

                     
        //-------------------------------- INDEX -----------------------------------//
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";
            
            //Create empty list
            IList<Event> events = new List<Event>();

            //If there is an "Admin" Logged in the session
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //Add all the events to the list
                events = context.Events.Include(c => c.Category).ToList();
                return View(events);
            }
            //If there is an regular "User" Logged in the session
            else if (HttpContext.Session.GetString("Type") == "user")
            {
                //Add all the events to the list Except Deleted ones
                events = context.Events.Where(c => c.Deleted == false).Include(c => c.Category).ToList();
                return View(events);
            }
            //If not...
            else
            {
                //return empty list with Error Message
                ViewBag.ErrorMessage = "You need to be Logged In to access this feature";
                return View(events);
            }
        }



        //-------------------------------- ADD EVENT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Add()
        {

            //If a Logged In "Admin" is accessing this feature
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //Display Add Form View
                AddEventViewModel addEventViewModel = new AddEventViewModel(context.Categories.ToList());
                return View(addEventViewModel);
                
            }
            else
            {
                //Return Error. Only Admins can add users to database
                TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });
            }


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


                // Success!!! event added...  return custom message
                TempData["Message"] = "Event " + newEvent.ID + " was successfully created.";
                TestFunctions.PrintConsoleMessage("SUCCESS, EVENT ADDED / CREATED");

                return Redirect("/Event");
            }

            addEventViewModel.SetCategories(context.Categories.ToList());
            return View(addEventViewModel);
        }



        //-------------------------------- REMOVE OR DELETE EVENT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Remove()
        {
            //If a Logged In "Admin" is accessing this feature
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //pass Non "deleted" and Non Protected events 
                ViewBag.events = context.Events.Where(c => (c.Deleted == false) && (c.Protected == false)).ToList();
                return View();
            }
            else
            {
                //Return Error. Only Admins can delete/remove users to database
                TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });
            }
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Remove(int[] eventsIds)
        {
            foreach (int eventId in eventsIds)
            {
                Event theEvent = context.Events.SingleOrDefault(c => c.ID == eventId);
                if (theEvent != null)
                {
                    theEvent.Deleted = true;
                }
            }

            context.SaveChanges();

            // Success!!! event deleted...  return custom message
            TempData["Message"] = "Event(s) successfully deleted.";
            TestFunctions.PrintConsoleMessage("SUCCESS, EVENT DELETED");

            return Redirect("/Event");
        }



        //-------------------------------- EDIT AN EVENT -----------------------------------//
        // GET: /<controller>/
        public IActionResult Edit(int eventId)
        {
            //If a Logged In "Admin" is accessing this feature
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                Event eventToEdit = context.Events.Where(c => c.Protected == false).SingleOrDefault(c => c.ID == eventId);

                if (eventToEdit != null)
                {
                    EditEventViewModel editEventViewModel = new EditEventViewModel(context.Categories.ToList())
                    {
                        EventID = eventToEdit.ID,
                        Name = eventToEdit.Name,
                        Description = eventToEdit.Description,
                        CategoryID = eventToEdit.CategoryID,
                        //Category = newEventCategory,
                        Price = eventToEdit.Price,
                        Date = eventToEdit.Date,
                        Time = eventToEdit.Date.TimeOfDay
                    };

                    return View(editEventViewModel);
                }
                else
                {
                    // The event does not exist in the Database or it is a protected event wich cannot be updted... return custom message
                    TempData["ErrorMessage"] = "Sorry, The event does not exist in the Database or it is a protected event wich cannot be updted.";
                    TestFunctions.PrintConsoleMessage("COULD NOT FIND THE EVENT IN THE DATABASE OR THE EVENT IS A PROTECTED ONE");
                }

                return Redirect("/Event");
            }
            else
            {
                //Return Error. Only Admins can edit users inside the database
                TempData["ErrorMessage"] = "This feature is reserved for Admins Only";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });
            }

        }

        // PUT: /<controller>/  ?????????????????????????????????????????????????????????
        [HttpPut]
        [HttpPost]
        public IActionResult Edit(EditEventViewModel modEvent)
        {
            if (ModelState.IsValid)
            {
                Event eventToEdit = context.Events.Where(c => c.Protected == false).SingleOrDefault(c => c.ID == modEvent.EventID);

                if(eventToEdit != null)
                {
                    EventCategory newEventCategory =context.Categories.Single(c => c.ID == modEvent.CategoryID);

                    // Modify the event with new information
                    eventToEdit.Name = modEvent.Name;
                    eventToEdit.Description = modEvent.Description;
                    eventToEdit.Category = newEventCategory;
                    eventToEdit.Price = modEvent.Price;
                    eventToEdit.Date = modEvent.Date + modEvent.Time;

                    context.Events.Update(eventToEdit);
                    context.SaveChanges();

                    // Success!!! event updated...  return custom message
                    TempData["Message"] = "Great!... the event was successfully updated.";
                    TestFunctions.PrintConsoleMessage("SUCCESS, EVENT UPDATED CORRECTLY");


                    return Redirect("/Event");
                }
                else
                {
                    // The event does not exist in the Database or it is a protected event wich cannot be updted... return custom message
                    ModelState.AddModelError("ServerError", "Sorry, The event does not exist in the Database or it is a protected event wich cannot be updted.");
                    modEvent.ServerError = true;
                    TestFunctions.PrintConsoleMessage("COULD NOT FIND THE EVENT IN THE DATABASE OR THE EVENT IS A PROTECTED ONE");
                }

            }
            return View(modEvent);
        }



        //----------------------------------- DETAILS -----------------------------------//
        // GET: /<controller>/
        public IActionResult Detail(int eventId)
        {
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            //If there is an "Admin" Logged in the session
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                // Do nothing... The admin can view any eventID
                Event selectedEvent = context.Events.Include(c => c.Category).Single(c => c.ID == eventId);

                Event eventToShow = new Event()
                {
                    ID = selectedEvent.ID,
                    Name = selectedEvent.Name,
                    Description = selectedEvent.Description,
                    Date = selectedEvent.Date,
                    Price = selectedEvent.Price,
                    Category = selectedEvent.Category,
                    Location = selectedEvent.Location??"",
                    Created = selectedEvent.Created,
                    Deleted = selectedEvent.Deleted
                };

                return View(eventToShow);
            }
            else
            {
                //Return Error. Only Admins can add users to database
                TempData["ErrorMessage"] = "You need to be Logged In to access this feature";
                return RedirectToAction("Index", new { username = HttpContext.Session.GetString("User") });
            }

            
        }



        //-------------------------------- UPDATE FROM API -----------------------------------//
        // GET: /<controller>/
        public IActionResult API()
        {
            if (HttpContext.Session.TryGetValue("User", out byte[] value))
            {
                string userType = HttpContext.Session.GetString("Type");

                if (userType == "admin")
                {
                    ViewBag.APIkey = TicketmasterAPIkey;
                    ViewBag.Mapkey = BingMapsAPIkey;
                    return View("UpdateFromAPI");
                }
            }

            //Unauthorize Access - Give Error and go Back to Dashboard
            ViewBag.UnauthorizedMessage = new string[] { "Sorry you are not authorized to access this feature, " +
                "You are being redirected to the User Login Page in a few seconds." };

            return RedirectToAction("Index", "Dashboard");
        }

        // Post: /<controller>/
        [HttpPost]
        public IActionResult API(IEnumerable<APIEventViewModel> incoming)
        {
            foreach (APIEventViewModel evento in incoming)
            {                                
                EventCategory newEventCategory = context.Categories.SingleOrDefault(c => c.Name.ToLower() == evento.Classification.ToLower());


                // If the category does not exists? create a new one
                if (newEventCategory == null)
                {
                    newEventCategory = new EventCategory
                    {
                        Name = evento.Classification
                    };

                    context.Categories.Add(newEventCategory);
                    context.SaveChanges();
                }
                                 
                // Add the new event to my existing events
                Event newEvent = new Event
                {
                    Name = evento.Name,
                    Description = evento.Description,
                    Category = newEventCategory,
                    Price = evento.Price,
                    Date = evento.StartDate + evento.StartTime.TimeOfDay,
                    Location = evento.Location,
                    Created = DateTime.Now
                };

                context.Events.Add(newEvent);               

                TestFunctions.PrintConsoleMessage(newEvent.Name);
            }

            context.SaveChanges();

            return Json(new { success = true, message = "Some message" });

        }


        //----------------------------------- Search -----------------------------------//
        // GET: /<controller>/
        public IActionResult Search()
        {

            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            SearchEventViewModel searchEventModel = new SearchEventViewModel(context.Categories.ToList());

            return View(searchEventModel);
        }

        // Post: /<controller>/
        [HttpPost]
        public IActionResult Results(SearchEventViewModel searchEventModel)
        {
            
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";



            if (searchEventModel.Value.Equals(""))
            {
                if (searchEventModel.Location.Equals("all") && searchEventModel.CategoryID.Equals(0))
                {
                    searchEventModel.Events = context.Events
                        .Include(c => c.Category).ToList();
                }
                else if (searchEventModel.Location.Equals("all"))
                {
                    searchEventModel.Events = context.Events
                        .Where(c => c.Name.ToLower().Contains(searchEventModel.Value.ToLower()))
                        .Where(cid => cid.CategoryID == searchEventModel.CategoryID)
                        .Include(c => c.Category).ToList();
                }
                else if (searchEventModel.CategoryID.Equals(0))
                {
                    searchEventModel.Events = context.Events
                        .Where(c => c.Name.ToLower().Contains(searchEventModel.Value.ToLower()))
                        .Where(l => l.Location.ToLower().Contains(searchEventModel.Location.ToLower()))
                        .Include(c => c.Category).ToList();
                }
                else
                {
                    searchEventModel.Events = context.Events
                        .Where(c => c.Name.ToLower().Contains(searchEventModel.Value.ToLower()))
                        .Where(l => l.Location.ToLower().Contains(searchEventModel.Location.ToLower()))
                        .Where(cid => cid.CategoryID == searchEventModel.CategoryID)
                        .Include(c => c.Category).ToList();
                }
            }
            else
            {
                if (searchEventModel.Location.Equals("all") && searchEventModel.CategoryID.Equals(0))
                {
                    searchEventModel.Events = context.Events.Where(c => c.Name.ToLower().Contains(searchEventModel.Value.ToLower())).Include(c => c.Category).ToList();
                }
                else if (searchEventModel.Location.Equals("all"))
                {
                    searchEventModel.Events = context.Events
                        .Where(cid => cid.CategoryID == searchEventModel.CategoryID)
                        .Include(c => c.Category).ToList();
                }
                else if (searchEventModel.CategoryID.Equals(0))
                {
                    searchEventModel.Events = context.Events
                        .Where(l => l.Location.ToLower().Contains(searchEventModel.Location.ToLower()))
                        .Include(c => c.Category).ToList();
                }
                else
                {
                    searchEventModel.Events = context.Events
                        .Where(l => l.Location.ToLower().Contains(searchEventModel.Location.ToLower()))
                        .Where(cid => cid.CategoryID == searchEventModel.CategoryID)
                        .Include(c => c.Category).ToList();
                }

            }

            /*
                Equals(searchEventModel.All) || searchEventModel.Value.Equals(""))
            {
                searchEventModel.Jobs = jobData.FindByValue(searchEventModel.Value);
            }
            else
            {
                searchEventModel.Jobs = jobData.FindByColumnAndValue(searchEventModel.Column, searchEventModel.Value);
            }
            */

            searchEventModel.SetCategories(context.Categories.ToList());
            return View("Search", searchEventModel);
        }







        //----------------------------------- BROCHURE -----------------------------------//
        // GET: /<controller>/
        public IActionResult Brochure(int eventId)
        {

            // Do nothing... The admin can view any eventID
            Event selectedEvent = context.Events.Include(c => c.Category).Single(c => c.ID == eventId);

            Event eventToShow = new Event()
            {
                ID = selectedEvent.ID,
                Name = selectedEvent.Name,
                Description = selectedEvent.Description,
                Date = selectedEvent.Date,
                Price = selectedEvent.Price,
                Category = selectedEvent.Category,
                Location = selectedEvent.Location??"",
                Created = selectedEvent.Created
            };

            return View(eventToShow);

        }

    }

}



