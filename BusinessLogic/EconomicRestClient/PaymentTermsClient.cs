using Contact_Form.EconomicRestClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Contact_Form.EconomicRestClient
{
    public class PaymentTermsClient : ClientHelper
    {
        private const bool traceOff = false;

        public static async Task<IEnumerable<PaymentTerm>> GetPaymentTerms()
        {
            IEnumerable<PaymentTerm> paymentTearms = new List<PaymentTerm>();

            using (var client = new HttpClient())
            {
                // try {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                HttpResponseMessage response = await client.GetAsync("/Payment-Terms");
                response.EnsureSuccessStatusCode();
                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                if (response.IsSuccessStatusCode)
                {
                    CollectionData<PaymentTerm> results = await response.Content.ReadAsAsync<CollectionData<PaymentTerm>>(formatters);
                    paymentTearms = results.Collection;

                    if (!traceOff)
                    {
                        Trace.Write("Loop PaymentTerms");

                        foreach (var paymentTerms in results.Collection)
                        {
                            Trace.Write("PaymentTermsNumber: " + paymentTerms.PaymentTermsNumber);
                            Trace.Write("Name: " + paymentTerms.Name);
                        }
                    }
                } else
                {
                    var content = response.Content.ReadAsStringAsync();
                    content.Wait();

                    throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                }
                // } catch (Exception ex) { }
            }

            return paymentTearms;
        }
    }
}