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
using System.Web.Caching;

namespace Contact_Form.EconomicRestClient
{
    public class CustomerClient : ClientHelper
    {
        private static bool isDemo = false;
        private static bool traceOff = false;

        private const string CACHE_PRODUCTPRICE = "PRODUCTPRICE_";

        public static IEnumerable<Customer> GetCustomers()
        {
            IEnumerable<Customer> customers = new List<Customer>();

            using (var client = new HttpClient())
            {
                // try {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                var responseTask = client.GetAsync(string.Format("customers{0}", isDemo ? "?demo=true" : string.Empty));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

                response.EnsureSuccessStatusCode();
                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                if (response.IsSuccessStatusCode)
                {
                    var resultTask = response.Content.ReadAsAsync<CollectionData<Customer>>(formatters);
                    resultTask.Wait();
                    CollectionData<Customer> collectionResult = resultTask.Result;
                    customers = collectionResult.Collection;

                    if (!traceOff)
                    {
                        Trace.Write("Loop customers");

                        foreach (var customer in collectionResult.Collection)
                        {
                            Trace.Write("Customer number: " + customer.CustomerNumber);
                            Trace.Write("Customer name: " + customer.Name);
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

            return customers;
        }

        public static Product GetProduct(int id)
        {
            Product product = null;
            string cache_name = string.Format("{0}{1}", CACHE_PRODUCTPRICE, Convert.ToString(id));

            if (HttpContext.Current != null)
            {
                product = (Product)HttpContext.Current.Cache[cache_name];
            }

            if (product == null)
            {
                using (var client = new HttpClient())
                {
                    // try {
                    client.BaseAddress = new Uri(ECONOMIC_URL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                    client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                    var responseTask = client.GetAsync(string.Format("products/{1}{0}", isDemo ? "?demo=true" : string.Empty, id));
                    responseTask.Wait();

                    HttpResponseMessage response = responseTask.Result;

                    var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                    if (response.IsSuccessStatusCode)
                    {
                        var resultTask = response.Content.ReadAsAsync<Product>(formatters);
                        resultTask.Wait();
                        product = resultTask.Result;

                        if (!traceOff)
                        {
                            Trace.Write("Loop products");

                            Trace.Write("Product number: " + product.ProductNumber);
                            Trace.Write("Product name: " + product.Name);
                        }
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // do nothing
                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync();
                        content.Wait();

                        throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                    }
                    // } catch (Exception ex) { }
                }

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Cache.Add(cache_name, product, null, DateTime.Now.AddMinutes(15), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                }
            }

            return product;
        }

        public static Customer GetCustomer(int customerId)
        {
            Customer customer = null;
            // Create HttpClientHandler and set UseDefaultCredentials property
            HttpClientHandler clientHandler = new HttpClientHandler();

            using (var client = new HttpClient(clientHandler))
            {
                // try {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                var responseTask = client.GetAsync(string.Format("customers/{1}{0}", isDemo ? "?demo=true" : string.Empty, customerId));
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;
                response.EnsureSuccessStatusCode();
                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                if (response.IsSuccessStatusCode)
                {
                    var customerResponseTask = response.Content.ReadAsAsync<Customer>(formatters);
                    customerResponseTask.Wait();
                    customer = customerResponseTask.Result;

                    if (!traceOff)
                    {
                        Trace.Write("Customer name: " + customer.Name);
                    }
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync();
                    content.Wait();

                    throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                }
                // } catch (Exception ex) { }
            }

            return customer;
        }

        public static async Task<IEnumerable<CustomerGroup>> GetCustomerGroups()
        {
            IEnumerable<CustomerGroup> customerGroups = new List<CustomerGroup>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                HttpResponseMessage response = await client.GetAsync(string.Format("customer-groups{0}", isDemo ? "?demo=true" : string.Empty));
                response.EnsureSuccessStatusCode();
                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                if (response.IsSuccessStatusCode)
                {
                    CollectionData<CustomerGroup> results = await response.Content.ReadAsAsync<CollectionData<CustomerGroup>>(formatters);
                    customerGroups = results.Collection;

                    if (!traceOff)
                    {
                        Trace.Write("Loop customer groups");
                        foreach (var group in results.Collection)
                        {
                            Trace.Write("Customer group number: " + group.CustomerGroupNumber);
                            Trace.Write("Customer name: " + group.Name);
                        }
                    }
                } else
                {
                    var content = response.Content.ReadAsStringAsync();
                    content.Wait();

                    throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                }
            }

            return customerGroups;
        }

        public static void InsertCustomer(CustomerInsert model)
        {
            IEnumerable<CustomerGroup> customerGroups = new List<CustomerGroup>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                var responseTask = client.PostAsJsonAsync("/Customers", model);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

                if (!response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync();
                    content.Wait();

                    throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                }
            }
        }

        public static void UpdateCustomer(int customerId, Customer model)
        {
            IEnumerable<CustomerGroup> customerGroups = new List<CustomerGroup>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ECONOMIC_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add(Header_Name_AccessID, AccessID);
                client.DefaultRequestHeaders.Add(Header_Name_AppID, AppID);

                var formatters = new List<MediaTypeFormatter>() {
                    new JsonMediaTypeFormatter(),
                    new XmlMediaTypeFormatter()
                };

                var responseTask = client.PutAsJsonAsync(string.Format("/Customers/{0}", customerId), model);
                responseTask.Wait();

                HttpResponseMessage response = responseTask.Result;

                if (!response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync();
                    content.Wait();

                    throw new Exception(string.Format("ErrorCode: {0} - Message: {1}", response.StatusCode, content.Result));
                }
            }
        }
    }
}