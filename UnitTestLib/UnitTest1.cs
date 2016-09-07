using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contact_Form.EconomicRestClient;
using Contact_Form.Controllers;
using Contact_Form.EconomicRestClient.Models;
using Contact_Form.Brainless;
using Contact_Form.EuropeBank;
using Contact_Form;
using Contact_Form.HexonetClient;

namespace UnitTestLib
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SignupCustomer()
        {
            var customerCtr = new CustomerController();
            //customerCtr.SignupCustomer(new Customer());
        }

        [TestMethod]
        public void GetCustomer()
        {
            CustomerClient.GetCustomer(9);
        }

        [TestMethod]
        public void BrainlessChangeUser()
        {
            var client = new BrainlessClient();
            //client.UpdateUser(3);
        }

        [TestMethod]
        public void PaymentTerms()
        {
            var result = PaymentTermsClient.GetPaymentTerms();

            result.Wait();
        }

        [TestMethod]
        public void VatZones()
        {
            var result = VatZonesClient.GetVatZones();

            result.Wait();
        }

        [TestMethod]
        public void GetLayouts()
        {
            var result = LayoutClient.GetLayouts();

            result.Wait();
        }

        [TestMethod]
        public void CheckDomain()
        {
            var c = new HexonetClient();
            var result = c.CheckDomain("dfgdfgfdg.dk");
            string test = string.Empty;
        }

        [TestMethod]
        public void GetCurrencyExchangeRate()
        {
            var c = new Helpers();
            var result = c.ConvertEuroTo(400, "DKK");
        }

        [TestMethod]
        public void GetProducts()
        {
            CustomerClient.GetProduct(433);
        }

        [TestMethod]
        public void InsertCustomer()
        {
            Customer c = new Customer()
            {
                Attention = new Attention()
                {
                    CustomerContactNumber = 1,
                    Self = "https://restapi.e-conomic.com/customers/9/contacts/1"
                },
                Address = string.Empty,
                Balance = 1,
                CiNumber = string.Empty,
                Barred = false,
                Country = "Danmark",
                County = "Danmark",
                CreditLimit = 1,
                CustomerContact = new CustomerContact()
                {
                    CustomerContactnumber = 1,
                    Self = "https://restapi.e-conomic.com/customers/9/contacts/1"
                },
                salesPerson = new SalesPerson()
                {
                    EmployeeNumber = 1,
                    Self = "https://restapi.e-conomic.com/employees/1"
                },
                CustomerNumber = 9,
                Ean = string.Empty,
                Email = string.Empty,
                Layout = new Layout()
                {
                    LayoutNumber = 18,
                    Name = string.Empty,
                    Self = "https://restapi.e-conomic.com/layouts/10"
                },
                PublicEntryNumber = string.Empty,
                TelephoneAndFaxNumber = string.Empty,
                VatNumber = string.Empty,
                Website = string.Empty,
                Zip = string.Empty,
                CustomerGroup = new CustomerGroup()
                {
                    CustomerGroupNumber = 3,
                    Name = "Websitepanel",
                    Self = "https://restapi.e-conomic.com/customer-groups/3"
                },
                Name = "test bruger 3",
                Currency = "DKK",
                PaymentTerms = new PaymentTerm()
                {
                    PaymentTermsNumber = 4,
                    Name = string.Empty,
                    Self = "https://restapi.e-conomic.com/payment-terms/4"
                },
                VatZone = new VatZone()
                {
                    VatZoneNumber = 1,
                    Name = string.Empty,
                    Self = "https://restapi.e-conomic.com/vat-zones/1"
                }
            };

            //CustomerClient.InsertCustomer(c);
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            Customer c = new Customer()
            {
                Attention = new Attention()
                {
                    CustomerContactNumber = 1,
                    Self = "https://restapi.e-conomic.com/customers/9/contacts/1"
                },
                Address = string.Empty,
                Balance = 1,
                CiNumber = string.Empty,
                Barred = false,
                Country = "Danmark",
                County = "Danmark",
                CreditLimit = 1,
                CustomerContact = new CustomerContact()
                {
                    CustomerContactnumber = 1,
                    Self = "https://restapi.e-conomic.com/customers/9/contacts/1"
                },
                salesPerson = new SalesPerson()
                {
                    EmployeeNumber = 1,
                    Self = "https://restapi.e-conomic.com/employees/1"
                },
                CustomerNumber = 9,
                Ean = string.Empty,
                Email = string.Empty,
                Layout = new Layout()
                {
                    LayoutNumber = 18,
                    Name = string.Empty,
                    Self = "https://restapi.e-conomic.com/layouts/10"
                },
                PublicEntryNumber = string.Empty,
                TelephoneAndFaxNumber = string.Empty,
                VatNumber = string.Empty,
                Website = string.Empty,
                Zip = string.Empty,
                CustomerGroup = new CustomerGroup()
                {
                    CustomerGroupNumber = 3,
                    Name = "Websitepanel",
                    Self = "https://restapi.e-conomic.com/customer-groups/3"
                },
                Name = "test bruger 55",
                Currency = "DKK",
                PaymentTerms = new PaymentTerm()
                {
                    PaymentTermsNumber = 4,
                    Name = string.Empty,
                    Self = "https://restapi.e-conomic.com/payment-terms/4"
                },
                VatZone = new VatZone()
                {
                    VatZoneNumber = 1,
                    Name = string.Empty,
                    Self = "https://restapi.e-conomic.com/vat-zones/1"
                }
            };

            CustomerClient.UpdateCustomer(9, c);
        }
    }
}
