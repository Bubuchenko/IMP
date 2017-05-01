using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMP_Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You forgot to provide a username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You forgot to provide a password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool Remember { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}