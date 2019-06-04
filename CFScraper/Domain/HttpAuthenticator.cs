using CFScraper.Contracts;
using CFScraper.Contracts.Constants;
using CFScraper.Domain.FormData;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace CFScraper.Domain
{
    internal class HttpAuthenticator
    {
        private LoginData _loginData;
        private HttpClient _httpClient;
        private FormDataSerializer _formDataSerializer;

        public HttpAuthenticator(HttpClient httpClient, FormDataSerializer formDataSerializer, string username, string password)
        {
            _loginData = new LoginData()
            {
                Username = username,
                Password = password
            };
            _httpClient = httpClient;
            _formDataSerializer = formDataSerializer;
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

        public async Task<bool> LoginAsync()
        {
            var loginData = GetLoginFormData();
            var loginResult = await _httpClient.PostAsync(CustomsForgeUrls.LOGIN_ACTION_PAGE, loginData);

            return LoginSuccessful(loginResult);
        }

        public bool Login()
        {
            return LoginAsync().Result;
        }

        private FormUrlEncodedContent GetLoginFormData()
        {
            return _formDataSerializer.Serialize(_loginData);
        }

        private bool LoginSuccessful(HttpResponseMessage httpResponse)
        {
            return httpResponse.StatusCode == System.Net.HttpStatusCode.OK
                && IsAuthenticated(httpResponse);
        }
    }
}
