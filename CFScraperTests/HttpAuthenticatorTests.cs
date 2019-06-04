using Microsoft.VisualStudio.TestTools.UnitTesting;
using CFScraper.Domain;
using System.Security;
using System.Net.Http;
using Moq;
using Moq.Protected;
using CFScraper.Contracts.Constants;
using CFScraper.Domain.FormData;

namespace CFScraperTests
{
    [TestClass]
    public class HttpAuthenticatorTests
    {
        [TestMethod]
        public void IsAuthenticated_ReturnsTrue_ForSearchPage()
        {
            var isAuthenticated = TestUrlForAuthentication(CustomsForgeUrls.SEARCH_HANDLER);
            Assert.IsTrue(isAuthenticated);
        }
        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_ForLoginPage()
        {
            var isAuthenticated = TestUrlForAuthentication(CustomsForgeUrls.LOGIN_PAGE);
            Assert.IsFalse(isAuthenticated);
        }

        private bool TestUrlForAuthentication(string url)
        {
            var mockResponse = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
            };

            var httpAuthenticator = new HttpAuthenticator(null, null, null, null);

            return httpAuthenticator.IsAuthenticated(mockResponse);
        }

        [TestMethod]
        public void Login_ReturnsFalse_OnFailedRequest()
        {
            var mockClientHandler = new MockHttpClient()
            {
                ResponseCode = System.Net.HttpStatusCode.BadRequest
            };

            var httpClient = mockClientHandler.GetMockClient();
            var serializer = new FormDataSerializer();
            var httpAuthenticator = new HttpAuthenticator(httpClient, serializer, "test", "test");

            Assert.IsFalse(httpAuthenticator.Login());
        }

        [TestMethod]
        public void Login_ReturnsFalse_OnFailedLogin()
        {
            var mockClientHandler = new MockHttpClient()
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, CustomsForgeUrls.LOGIN_PAGE)
            };

            var httpClient = mockClientHandler.GetMockClient();
            var serializer = new FormDataSerializer();
            var httpAuthenticator = new HttpAuthenticator(httpClient, serializer, "test", "test");

            Assert.IsFalse(httpAuthenticator.Login());
        }

        [TestMethod]
        public void Login_ReturnsTrue_OnSuccessfulLogin()
        {
            var mockClientHandler = new MockHttpClient()
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, CustomsForgeUrls.SEARCH_HANDLER)
            };

            var httpClient = mockClientHandler.GetMockClient();
            var serializer = new FormDataSerializer();
            var httpAuthenticator = new HttpAuthenticator(httpClient, serializer, "test", "test");

            Assert.IsTrue(httpAuthenticator.Login());
        }
    }
}
