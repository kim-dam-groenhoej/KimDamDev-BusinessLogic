using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class VatZone
    {
        [JsonProperty(PropertyName = "vatZoneNumber")]
        public int VatZoneNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "self")]
        public string Self { get; set; }
    }
}