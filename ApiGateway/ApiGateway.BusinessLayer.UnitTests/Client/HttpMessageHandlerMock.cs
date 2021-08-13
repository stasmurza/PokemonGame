using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.Client
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;

        public string Input { get; private set; }
        public int NumberOfCalls { get; private set; }

        public HttpMessageHandlerMock(string response = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;
            if (request.Content != null)
            {
                Input = await request.Content.ReadAsStringAsync();
            }
            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = _response is null ? null : new StringContent(_response)
            };
        }
    }
}
