using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace contact
{
    public static class CaptchaGet
    {
        [FunctionName("captcha-get")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "captcha")] HttpRequest req)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.textcaptcha.com/lauka.app.json");
            response.EnsureSuccessStatusCode();

            return new OkObjectResult(await response.Content.ReadAsStringAsync());
        }
    }
}
