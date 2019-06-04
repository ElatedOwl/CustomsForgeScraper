using CFScraper.Domain.FormData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Contracts
{
    internal class LoginData
    {
        [FormData("ips_username")]
        public string Username { get; set; }
        [FormData("ips_password")]
        public string Password { get; set; }
        [FormData("auth_key")]
        public string AuthKey { get; }
        public bool RememberMe => false;
        [FormData("rememberMe")]
        public int RememberMeForm => RememberMe ? 1 : 0;
    }
}
