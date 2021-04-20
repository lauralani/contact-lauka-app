using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

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
                SendGridClient client = new SendGridClient(Environment.GetEnvironmentVariable("SENDGRID_API_KEY"));
                EmailAddress from = new EmailAddress(Environment.GetEnvironmentVariable("SENDGRID_MAIL_FROM"));
                string subject = $"lauka.app contact request from {requestbody.Email}";
                EmailAddress to = new EmailAddress(Environment.GetEnvironmentVariable("SENDGRID_MAIL_TO"));
                string mailbody = $"Name: {requestbody.Name.Trim()}\n";
                mailbody += $"Email: {requestbody.Email}\n";
                mailbody += "---------\n\n";
                mailbody += $"{requestbody.Message}";

                SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, mailbody, null);
                Response response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    return new OkObjectResult("OK");
                }
                else
                {
                    return new OkObjectResult("MailSendError");
                }
                
            }
            else
            {
                return new OkObjectResult("InvalidCaptcha");
            }
        }
    }
}
