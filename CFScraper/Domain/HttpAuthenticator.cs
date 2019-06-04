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
        private string _password;
        private HttpClient _httpClient;

        public HttpAuthenticator(HttpClient httpClient, string username, string password)
        {
            _username = username;
            _password = password;
            _httpClient = httpClient;
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

        public bool Login()
        {
            throw new NotImplementedException();
        }
    }
}
