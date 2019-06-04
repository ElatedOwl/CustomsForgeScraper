using Microsoft.VisualStudio.TestTools.UnitTesting;
using CFScraper.Domain;
using System.Security;
using System.Net.Http;
using Moq;
using CFScraper.Contracts.Constants;

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

            var httpAuthenticator = new HttpAuthenticator(null, null, null);

            return httpAuthenticator.IsAuthenticated(mockResponse);
        }
    }
}
