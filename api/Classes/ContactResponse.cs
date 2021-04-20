using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace contact
{
    class ContactResponse
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }

        // add implicit cast to String to use the Class in HttpResponses cleanly
        public static implicit operator string(ContactResponse d) => JsonConvert.SerializeObject(d);
    }
}
