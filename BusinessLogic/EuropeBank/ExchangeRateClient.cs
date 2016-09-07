using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.Linq;
using System.Linq.Expressions;
using System.Globalization;
using System.Web.Caching;
using Contact_Form.EuropeBank.Models;

namespace Contact_Form.EuropeBank
{
    public class ExchangeRateClient
    {
        private static string url = "https://www.ecb.europa.eu";
        private static string EUROEXCHANGERATESXMLCACHENAME = "EuroExchangeRatesXml";
        private static bool traceOff = false;

        public static List<Cube> GetExchangeRates()
        {
            List<Cube> exchangeRates = null;
            Stream result = null;

            if (HttpContext.Current != null)
            {
                exchangeRates = (List<Cube>)HttpContext.Current.Cache[EUROEXCHANGERATESXMLCACHENAME];
            }

            if (exchangeRates == null)
            {
                exchangeRates = new List<Cube>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                    var responseClient = client.GetAsync("/stats/eurofxref/eurofxref-daily.xml");
                    responseClient.Wait();

                    HttpResponseMessage response = responseClient.Result;
                    response.EnsureSuccessStatusCode();
                    var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                    if (response.IsSuccessStatusCode)
                    {
                        var resultClient = response.Content.ReadAsStreamAsync();
                        resultClient.Wait();

                        using (StreamReader reader = new StreamReader(resultClient.Result, Encoding.UTF8))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.Load(reader);

                            var nsmgr = new XmlNamespaceManager(doc.NameTable);
                            nsmgr.AddNamespace("gesmes", "http://www.gesmes.org/xml/2002-08-01");

                            var elements = doc.GetElementsByTagName("Cube");

                            foreach (XmlNode cube in elements)
                            {
                                if (!cube.HasChildNodes)
                                {
                                    exchangeRates.Add(new Cube()
                                    {
                                        Currency = Convert.ToString(cube.Attributes["currency"].Value),
                                        Rate = Convert.ToDecimal(cube.Attributes["rate"].Value, new CultureInfo("en-US"))
                                    });
                                }
                            }

                            if (!traceOff)
                            {
                                foreach (var cube in exchangeRates)
                                {
                                    Trace.WriteLine("cubes");
                                    Trace.WriteLine("rate " + cube.Rate);
                                    Trace.WriteLine("currency " + cube.Currency);
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync();
                        content.Wait();

                        throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                    }
                }
            }

            if (HttpContext.Current != null)
            {
                HttpContext.Current.Cache.Add(EUROEXCHANGERATESXMLCACHENAME, exchangeRates, null, DateTime.Now.AddMinutes(15), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            
            return exchangeRates;
        }
    }
}