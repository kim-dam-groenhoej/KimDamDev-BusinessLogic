using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class AmountProduct
    {
        private IProductViewModel product;

        public IProductViewModel Product {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
            }
        }

        public int Amount { get; set; }
    }
}