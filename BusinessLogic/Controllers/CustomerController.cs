using Contact_Form.EconomicRestClient;
using Contact_Form.EconomicRestClient.Models;
using Contact_Form.Models;
using Contact_Form.WebsitepanelService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Contact_Form.Brainless;
using Contact_Form.Websitepanel.Auth;
using System.Globalization;
using System.Web.Security;

namespace Contact_Form.Controllers
{
    public class CustomerController : SurfaceController
    {
        private const string WEBSITEPANEL = "websitepanel";
        private const bool traceOff = true;
        private const string WEBSITEPANELUSERID = "websitepanelUserid";
        private const string ECONOMICID = "economicuserid";
        private const string BRAINLESSID = "brainlessid";
        private const string ESUSERSSOAP = "esUsersSoap";
        private const string ESAUTHENTICATIONSOAP = "esAuthenticationSoap";
        private const string ORDER_SESSION = "orderSession";
        private const string UMBRACOMEMBERSHIPPROVIDER = "UmbracoMembershipProvider";
        private const string WEBSITEPANEL_USERNAME = "";// insert here
        private const string WEBSITEPANEL_PASSWORD = ""; // insert here

        public Product GetProduct(int id)
        {
            return CustomerClient.GetProduct(id);
        }

        public ActionResult Signup(CustomerSignupViewModel profileModel)
        {
            TempData["successSignup"] = false;

            if (ModelState.IsValid)
            {
                OrderViewModel order = null;
                if (Session[WebshopSaleController.ORDER_SESSION] != null)
                {
                    order = (OrderViewModel)Session[WebshopSaleController.ORDER_SESSION];
                }
                else if (profileModel.IsProduct)
                {
                    throw new Exception("Der opstod en uventet fejl under oprettelsen af ordren. Prøv igen.");
                }

                if (Members.GetByUsername(profileModel.Username) != null)
                {
                    throw new Exception(string.Format("Brugernavn {0} er taget. Skriv et andet og prøv igen.", profileModel.Username));
                }

                if (Members.GetByEmail(profileModel.Email) != null)
                {
                    throw new Exception(string.Format("Email {0} er allerede brugt. Du har allerede en konto hos os.", profileModel.Email));
                }

                MembershipCreateStatus status = AddCustomerToUmbraco(profileModel);

                if (status != MembershipCreateStatus.Success)
                {
                    //
                    // economic
                    //
                    var isEmailInEconomic = CustomerClient.GetCustomers().FirstOrDefault(c => c.Email.Equals(profileModel.Email)) != null;
                    if (isEmailInEconomic)
                    {
                        throw new Exception(string.Format("Email {0} bliver allerede brugt i Kim Dam Developments regnskabssystem. Kontakt os for at få løst problemet.", profileModel.Email));
                    }

                    var customer = new CustomerInsert();
                    customer.Name = !string.IsNullOrWhiteSpace(profileModel.CompanyName) ? profileModel.CompanyName : string.Format("{0} {1}", profileModel.FirstName, profileModel.LastName);
                    customer.Address = profileModel.Address;
                    customer.City = profileModel.City;
                    customer.Zip = profileModel.Postcode;
                    customer.CorporateIdentificationNumber = profileModel.CVRNumber;
                    customer.Email = profileModel.Email;
                    customer.TelephoneAndFaxNumber = profileModel.Phone;
                    customer.Country = profileModel.Country;
                    customer.County = profileModel.Country;
                    customer.Currency = "DKK";
                    customer.PaymentTerms = new PaymentTerm()
                    {
                        PaymentTermsNumber = 4,
                        Name = string.Empty,
                        Self = "https://restapi.e-conomic.com/payment-terms/4"
                    };
                    customer.VatZone = new VatZone()
                    {
                        VatZoneNumber = 1,
                        Name = string.Empty,
                        Self = "https://restapi.e-conomic.com/vat-zones/1"
                    };
                    customer.Layout = new Layout()
                    {
                        LayoutNumber = 18,
                        Name = string.Empty,
                        Self = "https://restapi.e-conomic.com/layouts/10"
                    };
                    customer.Barred = false;

                    // economic insert
                    CustomerClient.InsertCustomer(customer);

                    if (order != null)
                    {
                        esUsersSoapClient client = null;

                        var product = CustomerClient.GetProduct(order.AmountProduct.Product.ID);

                        if (product.ProductType == ProductType.Webhotel)
                        {
                            client = new esUsersSoapClient(ESUSERSSOAP);
                            client.ClientCredentials.UserName.UserName = WEBSITEPANEL_USERNAME;
                            client.ClientCredentials.UserName.Password = WEBSITEPANEL_PASSWORD;
                            client.Open();

                            var userInfo = new WebsitepanelService.UserInfo();
                            userInfo.Address = profileModel.Address;
                            userInfo.City = profileModel.City;
                            userInfo.FirstName = profileModel.FirstName;
                            userInfo.LastName = profileModel.LastName;
                            userInfo.Zip = profileModel.Postcode;
                            userInfo.Email = profileModel.Email;
                            userInfo.CompanyName = profileModel.CompanyName;
                            userInfo.Comments = string.Format("CVR: {0}", profileModel.CVRNumber);

                            var regex = new System.Text.RegularExpressions.Regex(@"([\w+\s*\.*]+\))");

                            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                            {
                                RegionInfo region = new RegionInfo(ci.Name);

                                var match = regex.Match(ci.EnglishName);
                                string countryName = match.Value.Length == 0 ? "NA" : match.Value.Substring(0, match.Value.Length - 1);

                                if (countryName == profileModel.Country)
                                {
                                    userInfo.Country = region.TwoLetterISORegionName;
                                    break;
                                }
                            }

                            userInfo.Changed = DateTime.Now;
                            userInfo.Created = DateTime.Now;
                            userInfo.EcommerceEnabled = false;
                            userInfo.HtmlMail = true;
                            userInfo.Role = WebsitepanelService.UserRole.User;
                            userInfo.Status = WebsitepanelService.UserStatus.Pending;

                            client.AddUser(userInfo, true, profileModel.Password);

                        }
                        else if (product.ProductType == ProductType.GameServer)
                        {

                        }
                    }

                    TempData["successSignup"] = true;
                }

                return RedirectToCurrentUmbracoPage();
            }

            return CurrentUmbracoPage();
        }

        private System.Web.Security.MembershipCreateStatus AddCustomerToUmbraco(CustomerSignupViewModel profileModel)
        {
            var model = Members.CreateRegistrationModel("Customer");
            model.Password = profileModel.Password;
            model.Name = profileModel.Username;
            model.Username = profileModel.Username;
            model.Email = profileModel.Email;

            var status = System.Web.Security.MembershipCreateStatus.ProviderError;
            Members.RegisterMember(model, out status, false);
            return status;
        }

        [Authorize]
        public CustomerUpdateViewModel GetCustomer(Umbraco.Core.Models.IPublishedContent user)
        {
            int economicId = 0;
            if (user.Properties.FirstOrDefault(p => p.PropertyTypeAlias == ECONOMICID) != null && user.GetProperty(ECONOMICID).HasValue)
            {
                economicId = (int)user.GetProperty(ECONOMICID).Value;
            } else
            {
                throw new Exception("E-conomic er ikke blevet integreret. Kontakt os gerne, så vi kan løse problemet.");
            }

            var customer = CustomerClient.GetCustomer(economicId);

            var firstName = string.Empty;
            var lastName = string.Empty;
            if (!string.IsNullOrWhiteSpace(customer.Name))
            {
                var n = customer.Name.Split(' ');
                firstName = n.First();

                if (n.Count() > 1)
                {
                    for (int i = 1; i < n.Count(); i++)
                    {
                        lastName += i == 1 ? n[i] : string.Format(" {0}", n[i]);
                    }
                }      
            }

            return new CustomerUpdateViewModel()
            {
                Address = customer.Address,
                City = customer.City,
                CompanyName = !string.IsNullOrWhiteSpace(customer.CorporateIdentificationNumber) ? customer.Name : string.Empty,
                FirstName = firstName,
                LastName = lastName,
                ConfirmEmail = customer.Email,
                Email = customer.Email,
                Country = customer.Country,
                CVRNumber = customer.CorporateIdentificationNumber,
                Phone = customer.TelephoneAndFaxNumber,
                Postcode = customer.Zip
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UpdateProfile(CustomerUpdateViewModel profileModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = Members.GetCurrentMember();
                    int websitePanelId = 0;
                    int economicId = 0;
                    int brainLessUserId = 0;
                    DateTime timeNow = DateTime.Now;

                    if (user.Properties.FirstOrDefault(p => p.PropertyTypeAlias == WEBSITEPANELUSERID) != null && user.GetProperty(WEBSITEPANELUSERID).HasValue)
                    {
                        websitePanelId = (int)user.GetProperty(WEBSITEPANELUSERID).Value;
                    }

                    if (user.Properties.FirstOrDefault(p => p.PropertyTypeAlias == ECONOMICID) != null && user.GetProperty(ECONOMICID).HasValue)
                    {
                        economicId = (int)user.GetProperty(ECONOMICID).Value;
                    }

                    if (user.Properties.FirstOrDefault(p => p.PropertyTypeAlias == BRAINLESSID) != null && user.GetProperty(BRAINLESSID).HasValue)
                    {
                        brainLessUserId = (int)user.GetProperty(BRAINLESSID).Value;
                    }

                    if (Members.GetByEmail(profileModel.Email) != null)
                    {
                        throw new Exception(string.Format("Email {0} er allerede brugt. Kontakts os for nærmere information.", profileModel.Email));
                    }

                    esUsersSoapClient client = null;
                    esAuthenticationSoapClient clientAuth = null;

                    if (websitePanelId > 0)
                    {
                        client = new esUsersSoapClient(ESUSERSSOAP);
                        client.ClientCredentials.UserName.UserName = WEBSITEPANEL_USERNAME;
                        client.ClientCredentials.UserName.Password = WEBSITEPANEL_PASSWORD;
                        client.Open();


                        clientAuth = new esAuthenticationSoapClient(ESAUTHENTICATIONSOAP);
                        clientAuth.ClientCredentials.UserName.UserName = WEBSITEPANEL_USERNAME;
                        clientAuth.ClientCredentials.UserName.Password = WEBSITEPANEL_PASSWORD;
                        clientAuth.Open();
                    }

                    WebsitepanelService.UserInfo userInfo = null;
                    if (websitePanelId > 0)
                    {
                        userInfo = client.GetUserById(websitePanelId);
                        if (!string.IsNullOrWhiteSpace(profileModel.Address))
                        {
                            userInfo.Address = profileModel.Address;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.City))
                        {
                            userInfo.City = profileModel.City;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.FirstName))
                        {
                            userInfo.FirstName = profileModel.FirstName;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.LastName))
                        {
                            userInfo.LastName = profileModel.LastName;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Postcode))
                        {
                            userInfo.Zip = profileModel.Postcode;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Email))
                        {
                            userInfo.Email = profileModel.Email;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.CompanyName))
                        {
                            userInfo.CompanyName = profileModel.CompanyName;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.CVRNumber))
                        {
                            userInfo.Comments = string.Format("CVR: {0}", profileModel.CVRNumber);
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Country))
                        {
                            var regex = new System.Text.RegularExpressions.Regex(@"([\w+\s*\.*]+\))");

                            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
                            {
                                RegionInfo region = new RegionInfo(ci.Name);

                                var match = regex.Match(ci.EnglishName);
                                string countryName = match.Value.Length == 0 ? "NA" : match.Value.Substring(0, match.Value.Length - 1);

                                if (countryName == profileModel.Country)
                                {
                                    userInfo.Country = region.TwoLetterISORegionName;
                                    break;
                                }
                            }
                        }

                        userInfo.Changed = timeNow;
                    }

                    // websitepanel update
                    if (websitePanelId > 0)
                    {
                        var updateTask = client.UpdateUserAsync(userInfo);
                        if (profileModel.ConfirmPassword == profileModel.Password && !string.IsNullOrWhiteSpace(profileModel.Password))
                        {
                            if (Members.Login(user.Name, profileModel.CurrentPassword))
                            {
                                client.ChangeUserPassword(websitePanelId, profileModel.Password);
                            }
                            else
                            {
                                throw new Exception("Forkert Nuværende adgangskode. ");
                            }
                        }

                        updateTask.Wait(60000);
                    }

                    if (clientAuth != null)
                    {
                        clientAuth.Close();
                        clientAuth.Abort();
                    }

                    if (client != null)
                    {
                        client.Close();
                        client.Abort();
                    }

                    // economic get customer
                    System.Threading.Tasks.Task eUpdateCustomerTask = null;
                    System.Threading.Tasks.Task<Customer> eGetCustomerTask = null;
                    if (economicId > 0)
                    {
                        Customer customer = CustomerClient.GetCustomer(economicId);

                        if (!string.IsNullOrWhiteSpace(profileModel.CompanyName) || !string.IsNullOrWhiteSpace(profileModel.FirstName) || !string.IsNullOrWhiteSpace(profileModel.LastName))
                        {
                            if (!string.IsNullOrWhiteSpace(profileModel.CompanyName))
                            {
                                customer.Name = profileModel.CompanyName;
                            }
                            else
                            {
                                customer.Name = string.Format("{0} {1}", profileModel.FirstName, profileModel.LastName);
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(profileModel.Address))
                        {
                            customer.Address = profileModel.Address;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Email))
                        {
                            customer.Email = profileModel.Email;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.City))
                        {
                            customer.City = profileModel.City;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Country))
                        {
                            customer.Country = profileModel.Country;
                            customer.County = profileModel.Country;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Postcode))
                        {
                            customer.Zip = profileModel.Postcode;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.Phone))
                        {
                            customer.TelephoneAndFaxNumber = profileModel.Phone;
                        }
                        if (!string.IsNullOrWhiteSpace(profileModel.CVRNumber))
                        {
                            customer.CorporateIdentificationNumber = profileModel.CVRNumber;
                        }

                        // economic update
                        CustomerClient.UpdateCustomer(economicId, customer);
                    }

                    // brainless update
                    BrainLessUSChangeMemberInfo(profileModel, brainLessUserId);

                    // KimDamDev update
                    UmbracoChangePassword(profileModel, user);

                    TempData["success"] = true;

                    return RedirectToCurrentUmbracoPage("?success=true");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, new Exception(string.Format("{0}", ex.Message)));
                }
            }

            TempData["success"] = false;

            return CurrentUmbracoPage();
        }

        private static void BrainLessUSChangeMemberInfo(CustomerUpdateViewModel profileModel, int brainLessUserId)
        {
            if (brainLessUserId > 0)
            {
                BrainlessClient brainLessClient = new Brainless.BrainlessClient();
                brainLessClient.UpdateUser(brainLessUserId, profileModel);
            }
        }

        private void UmbracoChangePassword(CustomerUpdateViewModel profileModel, Umbraco.Core.Models.IPublishedContent user)
        {
            if (profileModel.ConfirmPassword == profileModel.Password && !string.IsNullOrWhiteSpace(profileModel.Password))
            {
                var result = Members.ChangePassword(user.Name, new Umbraco.Web.Models.ChangingPasswordModel()
                {
                    NewPassword = profileModel.Password,
                    OldPassword = profileModel.CurrentPassword,
                    Reset = false
                }, UMBRACOMEMBERSHIPPROVIDER);

                if (!result.Success)
                {
                    throw new Exception(string.Format("Forkert nuværende adgangskode. Prøv igen (Fejlkode: {0})", result.Result.ChangeError.ErrorMessage));
                }
            }
        }
    }
}