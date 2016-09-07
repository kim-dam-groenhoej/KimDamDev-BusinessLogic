using Contact_Form.EuropeBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form
{
    public class Helpers
    {
        public decimal ConvertEuroTo(decimal price, string currency)
        {
            var rates = ExchangeRateClient.GetExchangeRates();
            decimal newPrice = 0;
            decimal rate = 0;

            var found = false;
            int i = 0;
            while (!found && !(i > rates.Count - 1))
            {
                var eCurrency = Convert.ToString(rates[i].Currency);

                if (eCurrency.ToLower().Equals(currency.ToLower()))
                {
                    rate = rates[i].Rate;
                    found = true;
                }
                i++;
            }

            if (!found)
            {
                throw new Exception(string.Format("{0} not found", currency));
            }

            newPrice = price * rate;

            return newPrice;
        }
    }
}