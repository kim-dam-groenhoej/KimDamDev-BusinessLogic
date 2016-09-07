using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class PaymentTerm
    {
        [JsonProperty(PropertyName = "paymentTermsNumber")]
        public int PaymentTermsNumber { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "self")]
        public string Self { get; set; }
    }
}