using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        public AmountProduct AmountProduct { get; set; }

        public decimal GetTotalPrice
        {
            get
            {
                decimal price = 0;

                price = this.AmountProduct.Amount * this.AmountProduct.Product.Price;

                return price;
            }
        }
    }
}