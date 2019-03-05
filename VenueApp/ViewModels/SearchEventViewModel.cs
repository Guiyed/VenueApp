using VenueApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class SearchEventViewModel
    {
        // The search results
        public List<Event> Events { get; set; }

        // The search value
        [Display(Name = "Keyword:")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Value { get; set; } = "";

                
        // The column to search, defaults to all
        public EventCategory Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public double Price { get; set; }

        public string City { get; set; }
        public string Location { get; set; }


        [Display(Name = "Category")]
        public int CategoryID { get; set; }                             
        
        public List<SelectListItem> Categories { get; set; }
                


        public SearchEventViewModel()
        { }


        public SearchEventViewModel(IEnumerable<EventCategory> categories) {
            SetCategories(categories);             
        }

        public void SetCategories(IEnumerable<EventCategory> categories)
        {
            List<SelectListItem> CategoriesCopy = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = "0",
                    Text = "All",
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
