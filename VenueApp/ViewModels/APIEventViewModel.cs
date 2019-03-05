using VenueApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class APIEventViewModel : Event
    {
        // The search results
        public String Classification { get; set; }             
                
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
        
        public List<SelectListItem> Categories { get; set; }

        public bool ServerError { get; set; }
        
        public APIEventViewModel()
        { }


        public APIEventViewModel(IEnumerable<EventCategory> categories) {
            SetCategories(categories);             
        }

        public void SetCategories(IEnumerable<EventCategory> categories)
        {
            List<SelectListItem> CategoriesCopy = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "Select One",
                    Text = "Select One",
                    Selected = true,
                }
            };

            foreach (EventCategory category in categories)
            {

                CategoriesCopy.Add(new SelectListItem
                {
                    Value = ((int)category.ID).ToString(),
                    Text = category.Name.ToString(),
                    Selected = (category.Name.ToString().ToLower() == "none") ? true : false,
                });

            }

            this.Categories = CategoriesCopy;
        }
    }
}
