using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class CustomerSignupViewModel
    {
        public CustomerSignupViewModel()
        {
            this.IsProduct = false;
        }

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

        [Display(Name = "Dit brugernavn")]
        [RegularExpression("^(?=.{2,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "{0} er ikke et valid brugernavn. Kun a-z,A-Z,0-9 eller karaktere ._ er valid og mindst 2 tegn.")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Username { get; set; }

        [Display(Name = "Adgangskode")]
        [StringLength(80, MinimumLength = 7, ErrorMessage = "{0} skal være højere end 7 tegn")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Bekræft adgangskode")]
        [Compare("Password", ErrorMessage="{0} og {1} skal være ens")]
        [StringLength(80, MinimumLength = 7, ErrorMessage = "{0} skal være højere end 7 tegn")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Land")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Country { get; set; }

        public bool IsProduct { get; set; }
    }
}