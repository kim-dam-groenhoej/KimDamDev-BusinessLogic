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
    public class LayoutClient : ClientHelper
    {
        private const bool traceOff = false;

        public static async Task<IEnumerable<Layout>> GetLayouts()
        {
            IEnumerable<Layout> layouts = new List<Layout>();

            using (var client = new HttpClient())
            {
                // try {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                HttpResponseMessage response = await client.GetAsync("/LAYOUTS");
                response.EnsureSuccessStatusCode();
                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                if (response.IsSuccessStatusCode)
                {
                    CollectionData<Layout> results = await response.Content.ReadAsAsync<CollectionData<Layout>>(formatters);
                    layouts = results.Collection;

                    if (!traceOff)
                    {
                        Trace.Write("Loop layouts");

                        foreach (var vatZone in results.Collection)
                        {
                            Trace.Write("layout number: " + vatZone.LayoutNumber);
                            Trace.Write("Name: " + vatZone.Name);
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

            return layouts;
        }
    }
}