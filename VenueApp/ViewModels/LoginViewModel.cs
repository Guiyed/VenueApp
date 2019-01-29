﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VenueApp.ViewModels
{
    public class LoginViewModel
    {        
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
                
        [Required(ErrorMessage = "You must enter an alphanumeric password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
                
        public LoginViewModel()
        {

        }

    }
}
