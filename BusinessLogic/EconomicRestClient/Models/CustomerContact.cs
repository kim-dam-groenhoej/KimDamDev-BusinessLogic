using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class CustomerContact
    {
        [JsonProperty(PropertyName = "customerContactnumber")]
        public int CustomerContactnumber { get; set; }

        [JsonProperty(PropertyName = "self")]
        public string Self { get; set; }
    }
}