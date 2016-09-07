using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class ContactViewModel
    {
        [Display(Name = "Emne")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Subject { get; set; }

        [Display(Name = "Dit navn")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Name { get; set; }

        [Display(Name = "Evt. firmanavn")]
        public string CompanyName { get; set; }

        [Display(Name = "Din e-mail")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} er ikke en gyldig e-mail adresse.")]
        public string Email { get; set; }

        [Display(Name = "Bekræft din e-mail")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        [Compare("Email", ErrorMessage = "{0} er ikke ens med {1}")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Dit telefon nr")]
        public string Phone { get; set; }

        [Display(Name = "Din besked")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Message { get; set; }

        // Used to check for bots
        public string HelperField { get; set; }
    }
}