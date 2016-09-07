using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using Contact_Form.Models;

namespace Contact_Form.Brainless
{
    public class BrainlessClient
    {
        private MySqlConnection connection;

        public BrainlessClient()
        {
            connection = new MySqlConnection("server=<hostname>;User Id=<username>;password=<password here>;database=<database>;");
        }

        public void UpdateUser(int userId, CustomerUpdateViewModel customer)
        {
            connection.Open();
            var sql = "UPDATE Users SET ";

            var parameters = string.Empty;
            if (!string.IsNullOrWhiteSpace(customer.FirstName))
            {
                parameters += "firstname=@firstname,";
            }
            if (!string.IsNullOrWhiteSpace(customer.LastName))
            {
                parameters += "lastname=@lastname,";
            }
            if (!string.IsNullOrWhiteSpace(customer.Address))
            {
                parameters += "address=@address,";
            }
            if (!string.IsNullOrWhiteSpace(customer.Postcode))
            {
                parameters += "zip=@postCode,";
            }
            if (!string.IsNullOrWhiteSpace(customer.Phone))
            {
                parameters += "phone=@phone,";
            }
            if (!string.IsNullOrWhiteSpace(customer.City))
            {
                parameters += "city=@city,";
            }
            if (!string.IsNullOrWhiteSpace(customer.Country))
            {
                parameters += "country=@country,";
            }
            if (!string.IsNullOrWhiteSpace(customer.Email))
            {
                parameters += "email=@email,";
            }
            parameters = parameters.Remove(parameters.Length - 1);
            sql += parameters + " WHERE id=@id";

            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.Parameters.Add(new MySqlParameter("id", userId));
            cmd.Parameters.Add(new MySqlParameter("firstname", customer.FirstName));
            cmd.Parameters.Add(new MySqlParameter("lastname", customer.LastName));
            cmd.Parameters.Add(new MySqlParameter("address", customer.Address));
            cmd.Parameters.Add(new MySqlParameter("postCode", customer.Postcode));
            cmd.Parameters.Add(new MySqlParameter("city", customer.City));
            cmd.Parameters.Add(new MySqlParameter("phone", customer.Phone));
            cmd.Parameters.Add(new MySqlParameter("country", customer.Country));
            cmd.Parameters.Add(new MySqlParameter("email", customer.Email));

            cmd.CommandTimeout = 120;
            cmd.CommandType = System.Data.CommandType.Text;
            var results = cmd.ExecuteReader();
            while (results.Read())
            {
                string username = results.GetString("user");
            }

            connection.Close();
        }
    }
}