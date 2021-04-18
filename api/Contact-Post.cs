using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace contact
{
    public static class ContactPost
    {
        [FunctionName("contact-post")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contact")] HttpRequest req)
        {
            Console.WriteLine(req.Form["name"]);
            Console.WriteLine(req.Form["email"]);
            Console.WriteLine(req.Form["message"]);

            return new OkResult();
        }
    }
}
