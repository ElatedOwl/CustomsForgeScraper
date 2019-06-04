using CFScraper.Contracts.Constants;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CFScraperTests
{
    internal class MockHttpClient
    {
        public HttpStatusCode ResponseCode { get; set; } = HttpStatusCode.OK;
        public HttpRequestMessage RequestMessage { get; set; }
        public StringContent Content { get; set; }

        public HttpClient GetMockClient()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage request, CancellationToken cancellationToken) => GetMockResponse(request, cancellationToken));
            return new HttpClient(mockHttpMessageHandler.Object);
        }

        private Task<HttpResponseMessage> GetMockResponse(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(ResponseCode);

            if (Content != null)
                response.Content = Content;

            if (RequestMessage != null)
                response.RequestMessage = RequestMessage;

            return Task.FromResult(response);
        }
    }
}
