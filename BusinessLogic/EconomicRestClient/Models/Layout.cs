using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class Layout
    {
        [JsonProperty(PropertyName = "layoutNumber")]
        public int LayoutNumber { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "self")]
        [JsonIgnore]
        public string Self { get; set; }
    }
}