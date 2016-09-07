using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class CustomerUpdateViewModel
    {
        [Display(Name = "Dit navn")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string FirstName { get; set; }

        [Display(Name = "Dit efternavn")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string LastName { get; set; }

        [Display(Name = "Din e-mail")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "{0} er ikke en gyldig e-mail adresse.")]
        public string Email { get; set; }

        [Display(Name = "Bekræft din e-mail")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        [Compare("Email", ErrorMessage = "{0} er ikke ens med {1}")]
        public string ConfirmEmail { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Address { get; set; }

        [Display(Name = "Telefon")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Phone { get; set; }

        [Display(Name = "By")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string City { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Postcode { get; set; }

        [Display(Name = "Virksomhedsnavn")]
        public string CompanyName { get; set; }

        [Display(Name = "CVR")]
        public string CVRNumber { get; set; }

        [Display(Name = "Nuværende adgangskode")]
        public string CurrentPassword { get; set; }

        [Display(Name = "Adgangskode")]
        public string Password { get; set; }

        [Display(Name = "Bekræft adgangskode")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Country { get; set; }
    }
}