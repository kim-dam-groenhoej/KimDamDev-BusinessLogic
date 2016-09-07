using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contact_Form.EconomicRestClient.Models
{
    public class PostResult
    {
        public string[] errors { get; set; }

        public string HttpStatusCode { get; set; }

        public string Message { get; set; }
    }
}