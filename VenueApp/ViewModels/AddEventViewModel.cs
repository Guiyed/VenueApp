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
        [Required(ErrorMessage = "!! Please give your event an Awesome Name !!!")]
        [Display(Name = "Event Name")]
        [RegularExpression("^[a-zA-Z0-9 '-]{3,100}$", ErrorMessage = "Do not use special caracters and limit the name to 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your event a description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required]
        //[DataType(DataType.)]
        public double Price { get; set; }

        //[Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }                             
        
        [Required]
        public List<SelectListItem> Categories { get; set; }

        public bool ServerError { get; set; }


        public AddEventViewModel()
        { }


        public AddEventViewModel(IEnumerable<EventCategory> categories) {
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
