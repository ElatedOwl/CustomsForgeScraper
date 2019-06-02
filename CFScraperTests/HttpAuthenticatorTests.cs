using Microsoft.VisualStudio.TestTools.UnitTesting;
using CFScraper.Domain;
using System.Security;
using System.Net.Http;
using Moq;

namespace CFScraperTests
{
    [TestClass]
    public class HttpAuthenticatorTests
    {
        [TestMethod]
        public void IsAuthenticated_ReturnsFalse_ForLoginPage()
        {
            const string LOGIN_URL = "https://customsforge.com/index.php?app=core&module=global&section=login";
            var isAuthenticated = TestUrlForAuthentication(LOGIN_URL);
            Assert.IsFalse(isAuthenticated);
        }

        private bool TestUrlForAuthentication(string url)
        {
            var mockResponse = new HttpResponseMessage
            {
                RequestMessage = new HttpRequestMessage(HttpMethod.Get, url)
            };

            SecureString secureString = null;
            var httpAuthenticator = new HttpAuthenticator(null, null, secureString);

            return httpAuthenticator.IsAuthenticated(mockResponse);
        }
    }
}
