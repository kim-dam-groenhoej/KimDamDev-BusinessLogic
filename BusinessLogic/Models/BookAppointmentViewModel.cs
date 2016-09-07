using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class BookAppointmentViewModel
    {
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

        [Display(Name = "By")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string City { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Zip { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Address { get; set; }

        [Display(Name = "Dit telefon nr")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Phone { get; set; }

        [Display(Name = "Beskriv problemet")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public string Message { get; set; }

        [Display(Name = "Fjernsupport")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public bool IsRemoteSupport { get; set; }

        [Display(Name = "Hvornår er det bedst vi ringer?")]
        [Required(ErrorMessage = "{0} er ikke blevet udfyldt.")]
        public BestTimeOfDay BestTimeOfDay { get; set; }

        // Used to check for bots
        public string HelperField { get; set; }
    }

    public enum BestTimeOfDay
    {
        Morning = 0,
        Dinner = 1,
        AfterDinner = 2,
        Evening = 3
    }
}