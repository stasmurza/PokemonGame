using ApiGateway.BusinessLayer.Client.Config;
using ApiGateway.BusinessLayer.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.Client
{
    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IApiClientConfig _config;

        public ApiClient(IHttpClientFactory clientFactory, IApiClientConfig config)
        {
            if (clientFactory is null) throw new ArgumentNullException(nameof(clientFactory));
            if (config is null) throw new ArgumentNullException(nameof(config));

            _clientFactory = clientFactory;
            _config = config;
        }

        public async Task<string> GetAsync(string url)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            var client = _clientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMilliseconds(_config.Timeout);

            using (var response = await client.GetAsync(url))
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK ||
                    response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string error = (response.Content == null) ? String.Empty : await response.Content.ReadAsStringAsync();
                    throw new ApiBadRequestException(error);
                }
                else
                {
                    var error = $"The HTTP status code of the response was not expected ({response.StatusCode}).";
                    throw new ApiException(error);
                }
            }
        }
    }
}
