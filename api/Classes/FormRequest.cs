using System;
using System.Collections.Generic;
using System.Text;

namespace contact
{
    class FormRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Captcha { get; set; }
        public List<string> CaptchaHash { get; set; }
    }
}
