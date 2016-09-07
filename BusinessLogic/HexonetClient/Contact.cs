using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.HexonetClient
{
    public class Contact
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Organization { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public bool IsNew { get; set; }
    }
}