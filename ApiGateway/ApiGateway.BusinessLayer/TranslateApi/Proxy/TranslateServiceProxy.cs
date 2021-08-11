using ApiGateway.BusinessLayer.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.TranslateApi.Proxy
{
    public class TranslateServiceProxy : ITranslateServiceProxy
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<ITranslateServiceProxy> _logger;

        public TranslateServiceProxy(IApiClient apiClient, ILogger<ITranslateServiceProxy> logger)
        {
            if (apiClient is null) throw new ArgumentNullException(nameof(apiClient));
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<string> TranslateAsync(string url)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            try
            {
                return await _apiClient.GetAsync(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Translate API call failed.");
                throw;
            }
        }
    }
}
