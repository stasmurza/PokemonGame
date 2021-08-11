using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;
using System;

namespace ApiGateway.BusinessLayer.TranslateApi.Factory
{
    public class TranslateServiceFactory : ITranslateServiceFactory
    {
        private readonly ITranslateServiceProxy _proxy;
        private readonly IShakespeareApiConfig _shakespeareApiConfig;
        private readonly IYodaApiConfig _yodaApiConfig;
        private readonly ITranslateApiUrlBuilder _urlBuilder;
        private const string YodaHabitat = "cave";

        public TranslateServiceFactory(
            ITranslateServiceProxy proxy,
            IShakespeareApiConfig shakespeareApiConfig,
            IYodaApiConfig yodaApiConfig,
            ITranslateApiUrlBuilder urlBuilder)
        {
            if (proxy is null) throw new ArgumentNullException(nameof(proxy));
            if (shakespeareApiConfig is null) throw new ArgumentNullException(nameof(shakespeareApiConfig));
            if (yodaApiConfig is null) throw new ArgumentNullException(nameof(yodaApiConfig));
            if (urlBuilder is null) throw new ArgumentNullException(nameof(urlBuilder));

            _proxy = proxy;
            _shakespeareApiConfig = shakespeareApiConfig;
            _yodaApiConfig = yodaApiConfig;
            _urlBuilder = urlBuilder;
        }

        public ITranslateService CreateService(PokemonDto pokemon)
        {
            if (pokemon is null) throw new ArgumentNullException(nameof(pokemon));

            if (String.Equals(pokemon.Habitat, YodaHabitat, StringComparison.OrdinalIgnoreCase) ||
                pokemon.IsLegendary)
            {
                return new YodaTranslateService(_proxy, _yodaApiConfig, _urlBuilder);
            }
            else
            {
                return new ShakespeareTranslateService(_proxy, _shakespeareApiConfig, _urlBuilder);
            }
        }
    }
}
