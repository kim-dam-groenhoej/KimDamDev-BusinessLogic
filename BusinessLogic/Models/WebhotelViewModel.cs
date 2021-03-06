﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class WebhotelViewModel : IProductViewModel
    {
        public string Description
        {
            get; set;
        }

        public int ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }
    }
}