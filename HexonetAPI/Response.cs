using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexonetAPI
{
    public class Response
    {
        private Dictionary<string, string> result;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        internal Response()
        {
            this.result = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public Response(string response)
            : this()
        {
               this.Parse(response);
        }

        /// <summary>
        /// Parses the specified response.
        /// </summary>
        /// <param name="response">The response.</param>
        private void Parse(string response)
        {
            INI.INIFile file = new INI.INIFile(response);
            INI.Section section = file.FindSection("RESPONSE");

            foreach (var key in section.Keys)
            {
                try
                {
                    switch (key.Name.ToLower())
                    {
                        case "code":
                            this.Code = key.Value;
                            break;
                        case "description":
                            this.Description = key.Value;
                            break;
                        default:
                            this.result.Add(key.Name, key.Value);
                            break;
                    }
                    
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// As list.
        /// </summary>
        /// <returns></returns>
        public List<string> AsList()
        {
            List<string> lst = new List<string>();

            foreach (var key in result.Keys)
            {
                string value = "";

                if (result.TryGetValue(key, out value))
                {
                    lst.Add(String.Format("{0}={1}", key, value));
                }
            }

            return lst;
        }

        /// <summary>
        /// As array.
        /// </summary>
        /// <returns></returns>
        public string[] AsArray()
        {
            return this.AsList().ToArray();
        }

        /// <summary>
        /// Ases the dictionary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> AsDictionary()
        {
            return result;
        }

      
    }
}
