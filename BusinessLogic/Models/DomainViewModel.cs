using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class DomainViewModel : IProductViewModel
    {
        public DomainViewModel()
        {
            this.RegisterDomain = false;
            this.MoveDomain = false;
        }

        public string Tld { get; set; }

        public string Domain { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Boolean RegisterDomain { get; set; }

        public Boolean MoveDomain { get; set; }

        public String AuthCode { get; set; }

        public int ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }
    }
}