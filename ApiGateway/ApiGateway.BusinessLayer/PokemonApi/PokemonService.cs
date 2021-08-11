using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.PokemonApi.Config;
using ApiGateway.BusinessLayer.PokemonApi.Proxy;
using ApiGateway.BusinessLayer.PokemonApi.UrlBuilder;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.PokemonApi
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonServiceProxy _proxy;
        private readonly IPokemonApiConfig _config;
        private readonly IPokemonApiUrlBuilder _urlBuilder;

        public PokemonService(IPokemonServiceProxy proxy, IPokemonApiConfig config, IPokemonApiUrlBuilder urlBuilder)
        {
            if (proxy is null) throw new ArgumentNullException(nameof(proxy));
            if (config is null) throw new ArgumentNullException(nameof(config));
            if (urlBuilder is null) throw new ArgumentNullException(nameof(urlBuilder));

            _proxy = proxy;
            _config = config;
            _urlBuilder = urlBuilder;
        }

        public async Task<PokemonDto> GetPokemonByNameAsync(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            var url = _urlBuilder.ByNameUrl(_config.Url, name);
            var response = await _proxy.GetPokemonByNameAsync(url);
            var pokemon = JsonConvert.DeserializeObject<PokemonDto>(response);

            return pokemon;
        }
    }
}
