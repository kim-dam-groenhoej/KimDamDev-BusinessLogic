using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class LoginModelViewModel
    {
        public LoginModelViewModel()
        {
            
        }

        [Display(Name = "Brugernavn")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public String Username { get; set; }

        [Display(Name = "Adgangskode")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public String Password { get; set; }

        public string HelperField { get; set; }

        public int RedirectPage { get; set; }

        public bool RememberMe { get; set; }
    }
}