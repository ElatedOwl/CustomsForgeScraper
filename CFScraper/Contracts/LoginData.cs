using System;
using System.Collections.Generic;
using System.Text;

namespace CFScraper.Contracts
{
    internal class LoginData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AuthKey { get; }
        public bool RememberMe => false;
    }
}
