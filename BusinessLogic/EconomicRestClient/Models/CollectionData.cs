using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class CollectionData<T>
    {
        public IEnumerable<T> Collection { get; set; }
    }
}