using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.Models
{
    public class GameServerViewModel : IProductViewModel
    {
        public GameServerViewModel()
        {
            this.Slots = 12;
        }

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

        public int Slots
        {
            get; set;
        }
    }
}