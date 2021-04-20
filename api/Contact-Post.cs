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
using Newtonsoft.Json;

namespace contact
{
    public static class ContactPost
    {
        [FunctionName("contact-post")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "contact")] HttpRequest req)
        {
            StreamReader sr = new StreamReader(req.Body);

            FormRequest requestbody;
            try
            {
                requestbody = JsonConvert.DeserializeObject<FormRequest>(await sr.ReadToEndAsync());
            }
            catch
            {
                return new OkObjectResult("InvalidRequest");
            }

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(requestbody.Captcha.Trim().ToLower());
            byte[] result = md5.ComputeHash(textToHash);
            string hashedcaptcha = BitConverter.ToString(result).Replace("-", null).ToLower();

            if (requestbody.CaptchaHash.Contains(hashedcaptcha))
            {
                //send mail

                return new OkObjectResult("OK");
            }
            else
            {
                return new OkObjectResult("InvalidCaptcha");
            }
        }
    }
}
