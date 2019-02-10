﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]{1,12}$", ErrorMessage = "User must be between 1 and 12 alphanumeric characters")]
        [Display(Name = "Event Category Name")]
        public string Name { get; set; }
        

    }
}