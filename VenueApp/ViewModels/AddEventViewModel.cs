using VenueApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class AddEventViewModel
    {
        [Required]
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your event a description")]
        public string Description { get; set; }

        //[Required]
        public DateTime Date { get; set; }

        //[Required]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }                             
        
        public List<SelectListItem> Categories { get; set; }


        public AddEventViewModel()
        { }


        public AddEventViewModel(IEnumerable<EventCategory> categories) {

            Categories = new List<SelectListItem>();

            foreach (EventCategory category in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = ((int)category.ID).ToString(),
                    Text = category.Name.ToString()
                });

            }              

        }
    }
}
