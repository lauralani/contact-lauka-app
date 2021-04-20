using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
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
            string name = req.Form["name"].ToString().Trim();
            string email = req.Form["email"];
            string body = req.Form["message"];
            List<string> captcha = JsonConvert.DeserializeObject<List<string>>(req.Form["captcha-hash"]);

            MD5 md5 = new MD5CryptoServiceProvider();
            string input = req.Form["captcha"].ToString().Trim().ToLower();
            byte[] textToHash = Encoding.Default.GetBytes(input);
            byte[] result = md5.ComputeHash(textToHash);
            string hashedcaptcha = BitConverter.ToString(result).Replace("-", null).ToLower();

            if (captcha.Contains(hashedcaptcha))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }


            return new OkResult();
        }
    }
}
