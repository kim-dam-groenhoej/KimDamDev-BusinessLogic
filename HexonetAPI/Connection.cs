using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace HexonetAPI
{
    public class Connection
    {

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        /// <value>
        /// The login.
        /// </value>
        public string  Login { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="URL">The URL.</param>
        /// <param name="Entity">The entity.</param>
        /// <param name="Login">The login.</param>
        /// <param name="Password">The password.</param>
        public Connection(string URL, string Entity, string Login, string Password)
            : this()
        {
            this.URL = URL;
            this.Entity = Entity;
            this.Login = Login;
            this.Password = Password;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="params">The @params.</param>
        public Connection(IDictionary<string, string> @params)
            : this()
        {
            string sURL;
            string sEntity;
            string sLogin;
            string sPassword;
            
            @params.TryGetValue("URL", out  sURL);
            @params.TryGetValue("Entity", out sEntity);
            @params.TryGetValue("Login", out sLogin);
            @params.TryGetValue("Passowrd", out sPassword);

            this.URL = sURL;
            this.Entity = sEntity;
            this.Login = sLogin;
            this.Password = sPassword;
        }

    
        /// <summary>
        /// Requests the raw data.
        /// </summary>
        /// <param name="Command">The command.</param>
        public string RequestRawData(IDictionary<string, string> Command)
        {
            string sCommand = "";
            string postData = "";

            foreach (var key in Command.Keys)
            {
                string value;
                if (Command.TryGetValue(key, out value))
                {
                    sCommand += key + "=" + value + "\n";
                }
            }

            //postData += "s_entity=" + this.Entity + "&";
            postData += "s_login=" + this.Login + "&";
            postData += "s_pw=" + this.Password + "&";
            postData += "s_command=" + sCommand;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(this.URL);
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = byteArray.Length;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";

            Stream dataStream = httpWebRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
          
            using (WebResponse response = httpWebRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Requests the specified command.
        /// </summary>
        /// <param name="Command">The command.</param>
        /// <returns></returns>
        public Response Request(IDictionary<string, string> Command)
        {
            string request = RequestRawData(Command);
            return new Response(request);
        }

    }
}
