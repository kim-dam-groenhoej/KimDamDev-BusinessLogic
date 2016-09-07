using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class Customer
    {
        [JsonProperty(PropertyName = "attention")]
        [JsonIgnore]
        public Attention Attention { get; set; }

        [JsonProperty(PropertyName = "customerNumber")]
        [JsonIgnore]
        public int CustomerNumber { get; set; }

        [JsonProperty(PropertyName = "balance")]
        public decimal Balance { get; set; }

        [JsonProperty(PropertyName = "barred")]
        public bool Barred { get; set; }

        [JsonProperty(PropertyName = "creditLimit")]
        public decimal CreditLimit { get; set; }

        [JsonProperty(PropertyName = "customerContact")]
        [JsonIgnore]
        public CustomerContact CustomerContact { get; set; }

        [JsonProperty(PropertyName = "ean")]
        public string Ean { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "ciNumber")]
        public string CiNumber { get; set; }

        [JsonProperty(PropertyName = "vatZone")]
        public VatZone VatZone { get; set; }

        [JsonProperty(PropertyName = "layout")]
        public Layout Layout { get; set; }

        [JsonProperty(PropertyName = "paymentTerms")]
        public PaymentTerm PaymentTerms { get; set; }

        [JsonProperty(PropertyName = "publicEntryNumber")]
        public string PublicEntryNumber { get; set; }

        [JsonProperty(PropertyName = "salesPerson")]
        [JsonIgnore]
        public SalesPerson salesPerson { get; set; }

        [JsonProperty(PropertyName = "telephoneAndFaxNumber")]
        public string TelephoneAndFaxNumber { get; set; }

        [JsonProperty(PropertyName = "vatNumber")]
        public string VatNumber { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "county")]
        public string County { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "customerGroup")]
        public CustomerGroup CustomerGroup { get; set; }

        [JsonProperty(PropertyName = "corporateIdentificationNumber")]
        public string CorporateIdentificationNumber { get; set; }
    }
}