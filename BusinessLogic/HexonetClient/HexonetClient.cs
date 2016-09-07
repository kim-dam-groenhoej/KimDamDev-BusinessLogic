using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HexonetAPI;
using System.Diagnostics;

namespace Contact_Form.HexonetClient
{
    public class HexonetClient
    {
        private bool _isDemo = true;
        private string _url = "https://coreapi.1api.net/api/call.cgi";
        private string _username;
        private string _password;
        private string _entity;

        public HexonetClient()
        {
            if (_isDemo)
            {
                _username = ""; // insert here
                _password = "";// insert here
                _entity = "";// insert here
            } else
            {
                _username = "";// insert here
                _password = "!";// insert here
                _entity = "";// insert here
            }
        }

        public DomainStatus CheckDomain(string domain)
        {
            var domainStatus = DomainStatus.None;

            HexonetAPI.Connection connection = new HexonetAPI.Connection(_url, _entity, _username, _password);

            Dictionary<string, string> command = new Dictionary<string, string>();
            command.Add("COMMAND", "CheckDomain");
            command.Add("DOMAIN", domain);
            Response response = connection.Request(command);

            try {
                int code = Convert.ToInt32(response.Code);
                domainStatus = (DomainStatus)code;
            } catch (Exception ex)
            {
                ThrowError(response, ex);
            }

            return domainStatus;
        }

        public ContactHandleStatus AddContact(Contact contact)
        {
            var contactStatus = ContactHandleStatus.None;

            HexonetAPI.Connection connection = new HexonetAPI.Connection(_url, _entity, _username, _password);

            Dictionary<string, string> command = new Dictionary<string, string>();
            command.Add("COMMAND", "AddContact");
            command.Add("title", contact.Title);
            command.Add("firstname", contact.FirstName);
            command.Add("middlename", contact.MiddleName);
            command.Add("lastname", contact.LastName);
            command.Add("organization", contact.Organization);
            command.Add("street", contact.Street);
            command.Add("city", contact.City);
            command.Add("state", contact.State);
            command.Add("zip", contact.Zip);
            command.Add("country", contact.Country);
            command.Add("phone", contact.Phone);
            command.Add("fax", contact.Fax);
            command.Add("email", contact.Email);
            command.Add("new", contact.IsNew ? "1" : "0");
            Response response = connection.Request(command);

            try
            {
                int code = Convert.ToInt32(response.Code);
                contactStatus = (ContactHandleStatus)code;
            }
            catch (Exception ex)
            {
                ThrowError(response, ex);
            }

            return contactStatus;
        }

        private static void ThrowError(Response response, Exception ex)
        {
            throw new Exception(string.Format("Code: {0} - Error: {1} - Exception message: {2} - Stacktrace: {3}", response.Code, response.Description, ex.Message, ex.StackTrace));
        }

        public DomainStatus RegisterDomain(string domain, int years, Contact contact)
        {
            var domainStatus = DomainStatus.None;

            HexonetAPI.Connection connection = new HexonetAPI.Connection(_url, _entity, _username, _password);

            Dictionary<string, string> command = new Dictionary<string, string>();
            command.Add("COMMAND", "CheckDomain");
            command.Add("DOMAIN", domain);
            Response response = connection.Request(command);

            try
            {
                int code = Convert.ToInt32(response.Code);
                domainStatus = (DomainStatus)code;
            }
            catch (Exception ex)
            {
                ThrowError(response, ex);
            }

            return domainStatus;
        }
    }
}