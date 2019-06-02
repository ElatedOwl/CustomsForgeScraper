using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Text;

namespace CFScraper.Domain
{
    internal class HttpAuthenticator
    {
        private string _username;
        private SecureString _password;
        private HttpClient _httpClient;

        public HttpAuthenticator(HttpClient httpClient, string username, SecureString password)
        {
            _username = username;
            _password = password;
            _httpClient = httpClient;
        }

        [Obsolete("Storing passwords as string not secure, recommend using SecureString")]
        public HttpAuthenticator(HttpClient httpClient, string username, string password)
        {
            _username = username;
            _password = ConvertStringToSecureString(password);
            _httpClient = httpClient;
        }

        private SecureString ConvertStringToSecureString(string s)
        {
            var secureString = new SecureString();

            var characters = s.ToCharArray();

            foreach (var character in characters)
                secureString.AppendChar(character);

            secureString.MakeReadOnly();

            return secureString;
        }

        public bool IsAuthenticated(HttpResponseMessage httpResponse)
        {
            var onLoginPage = httpResponse
                .RequestMessage
                .RequestUri
                .ToString()
                .ToLower()
                .Contains("section=login");

            return !onLoginPage;
        }
    }
}
