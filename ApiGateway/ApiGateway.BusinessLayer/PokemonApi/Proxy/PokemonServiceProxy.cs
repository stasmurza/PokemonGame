using ApiGateway.BusinessLayer.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.PokemonApi.Proxy
{
    public class PokemonServiceProxy : IPokemonServiceProxy
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger<IPokemonServiceProxy> _logger;
        
        public PokemonServiceProxy(IApiClient apiClient, ILogger<IPokemonServiceProxy> logger)
        {
            if (apiClient is null) throw new ArgumentNullException(nameof(apiClient));
            if (logger is null) throw new ArgumentNullException(nameof(logger));

            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<string> GetPokemonByNameAsync(string url)
        {
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url));

            try
            {
                return await _apiClient.GetAsync(url);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Pokemon API call failed");
                throw;
            }
        }
    }
}
