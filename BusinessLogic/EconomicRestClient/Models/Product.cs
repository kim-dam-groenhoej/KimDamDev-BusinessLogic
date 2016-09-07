using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "barred")]
        public bool Barred { get; set; }

        [JsonProperty(PropertyName = "costPrice")]
        public decimal CostPrice { get; set; }

        [JsonProperty(PropertyName = "productNumber")]
        public int ProductNumber { get; set; }

        [JsonProperty(PropertyName = "productGroup")]
        public ProductGroup ProductGroup { get; set; }

        [JsonProperty(PropertyName = "salesPrice")]
        public decimal SalesPrice { get; set; }

        public ProductType ProductType
        {
            get
            {
                var pType = ProductType.None;
                switch (this.ProductNumber)
                {
                    case 11:
                        pType = ProductType.GameServer;
                        break;
                    case 12:
                        pType = ProductType.GameServer;
                        break;
                    case 13:
                        pType = ProductType.GameServer;
                        break;
                    case 14:
                        pType = ProductType.GameServer;
                        break;
                    case 15:
                        pType = ProductType.GameServer;
                        break;
                    case 16:
                        pType = ProductType.GameServer;
                        break;
                    case 9:
                        pType = ProductType.Webhotel;
                        break;
                    case 10:
                        pType = ProductType.Webhotel;
                        break;
                }

                return pType;
            }
        }
    }

    public enum ProductType
    {
        None,
        GameServer,
        Webhotel,
        Domain
    }
}