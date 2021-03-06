﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Form.Models
{
    public interface IProductViewModel
    {
        int ID { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        decimal Price { get; set; }
    }
}
