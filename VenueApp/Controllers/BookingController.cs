using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VenueApp.Data;
using VenueApp.Helpers;
using VenueApp.Models;
using VenueApp.ViewModels;



namespace VenueApp.Controllers
{
    public class BookingController : Controller
    {
        private VenueAppDbContext context;

        public BookingController(VenueAppDbContext dbContext)
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
            IList<Booking> bookings = new List<Booking>();

            //If there is an "Admin" Logged in the session
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                //Add all the users to the list
                bookings = context.Bookings.Include(c => c.User).Include(c => c.Event).ToList();
                return View(bookings);
            }
            //If there is an regular "User" Logged in the session
            else if (HttpContext.Session.GetString("Type") == "user")
            {
                //Show his details only
                bookings = context.Bookings.Where(c => c.UserID == HttpContext.Session.GetInt32("UserID")).Include(c => c.User).Include(c => c.Event).ToList();
                return View(bookings);
            }
            //If not...
            else
            {
                //return empty list with Error Message
                ViewBag.ErrorMessage = "You need to be Logged In to access this feature";
                return View(bookings);
            }
        }



        //-------------------------------- SCHEDULED (It is like a Method) -----------------------------------//
        // GET: /<controller>/
        public IActionResult Scheduled(int userId)
        {
            ViewBag.Message = TempData["Message"] ?? "";
            ViewBag.ErrorMessage = TempData["ErrorMessage"] ?? "";

            User currentUser = context.Users.SingleOrDefault(c => c.ID == userId);

            List<Booking> scheduledEvents = context
                .Bookings
                .Include(item => item.Event)
                .Where(cm => cm.UserID == userId)
                .ToList();

            ViewScheduledViewModel scheduledViewModel = new ViewScheduledViewModel
            {
                User = currentUser,
                Bookings = scheduledEvents
            };

            return View(scheduledViewModel);
        }



        //-------------------------------- ADD BOOKING BY USER -----------------------------------//
        // GET: /<controller>/
        public IActionResult Add(int userId)
        {
            User selectedUser = context.Users.SingleOrDefault(c => c.ID == userId);
            List<Event> events = context.Events.ToList();

            BookingViewModel addBookingViewModel = new BookingViewModel(selectedUser, events);
            return View(addBookingViewModel);
        }

        [HttpPost]
        public IActionResult Add(BookingViewModel addBookingViewModel)
        {
            if (ModelState.IsValid)
            {

                IList<Booking> existingItems = context.Bookings
                    .Where(cm => cm.EventID == addBookingViewModel.EventID)
                    .Where(cm => cm.UserID == addBookingViewModel.UserID).ToList();


                if (existingItems.Count == 0)   //Do not duplicate events in the User bookings
                {
                    // Add the new booking to my existing database
                    Booking newBooking = new Booking
                    {
                        EventID = addBookingViewModel.EventID,
                        UserID = addBookingViewModel.UserID
                    };

                    context.Bookings.Add(newBooking);
                    context.SaveChanges();

                    // Success!!! new event booked...  return custom message
                    TempData["Message"] = "Event Successfully Booked.";
                    TestFunctions.PrintConsoleMessage("SUCCESS, BOOKING DELETED");

                    return RedirectToAction("Scheduled", new { userId = newBooking.UserID});
                }

                // Error!!! booking ID already in database...  return custom message
                TempData["ErrorMessage"] = "Sorry, the event you are triying to Book is already in the users reservations.";
                TestFunctions.PrintConsoleMessage("WARNING, BOOKING ALREADY IN DATABASE");
                return RedirectToAction("Scheduled", new { userId = addBookingViewModel.UserID });
            }

            return View(addBookingViewModel);
        }



        //-------------------------------- REMOVE OR DELETE BOOKING BY USER (ADMINS Only)-----------------------------------//
        // GET: /<controller>/
        public IActionResult Delete(int userId, int eventId)
        {
            User selectedUser = context.Users.SingleOrDefault(c => c.ID == userId);
            Event selectedEvent = context.Events.SingleOrDefault(c => c.ID == eventId);

            BookingViewModel bookingToBeDeleted = new BookingViewModel
            {
                UserID = selectedUser.ID,
                User = selectedUser,
                EventID = selectedEvent.ID,
                Event = selectedEvent
            };

        return View(bookingToBeDeleted);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult Delete(BookingViewModel deleteBookingViewModel)
        {
            Booking theBooking = context.Bookings.SingleOrDefault(c => (c.UserID == deleteBookingViewModel.UserID) &&(c.EventID == deleteBookingViewModel.EventID));
            if (theBooking != null)
            {
                context.Bookings.Remove(theBooking);
                context.SaveChanges();

                // Success!!! booking deleted...  return custom message
                TempData["Message"] = "Booking successfully deleted.";
                TestFunctions.PrintConsoleMessage("SUCCESS, BOOKING DELETED");
            }
            else
            {
                // Error!!! booking not found...  return custom message
                TempData["Message"] = "Booking not found. No changes has been made to the database";
                TestFunctions.PrintConsoleMessage("ERROR, NO BOOKING FOUND");
            }

            return Redirect("/Booking");
        }



        //-------------------------------- REMOVE OR DELETE BOOKING -----------------------------------//
        // GET: /<controller>/
        public IActionResult DeleteBy(int userId)
        {
            User selectedUser = context.Users.SingleOrDefault(c => c.ID == userId);
            List<Event> events = context.Events.ToList();

            BookingViewModel showBookingViewModel = new BookingViewModel(selectedUser, events);
            return View(showBookingViewModel);
        }

        // POST: /<controller>/
        [HttpPost]
        public IActionResult DeleteBy(BookingViewModel deleteBookingViewModel)
        {
            Booking theBooking = context.Bookings.SingleOrDefault(c => (c.UserID == deleteBookingViewModel.UserID) && (c.EventID == deleteBookingViewModel.EventID));
            if (theBooking != null)
            {
                context.Bookings.Remove(theBooking);
                context.SaveChanges();

                // Success!!! booking deleted...  return custom message
                TempData["Message"] = "Booking successfully deleted.";
                TestFunctions.PrintConsoleMessage("SUCCESS, BOOKING DELETED");
            }
            else
            {
                // Error!!! booking not found...  return custom message
                TempData["Message"] = "Booking not found. No changes has been made to the database";
                TestFunctions.PrintConsoleMessage("ERROR, NO BOOKING FOUND");
            }

            return RedirectToAction("Scheduled", new { userId = deleteBookingViewModel.UserID });
        }



    }
}