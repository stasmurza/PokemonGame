using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Models;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.TranslateApi
{
    public class TranslateService : ITranslateService
    {
        private readonly ITranslateServiceProxy _proxy;
        private readonly ITranslateApiConfig _config;
        private readonly ITranslateApiUrlBuilder _urlBuilder;

        public TranslateService(ITranslateServiceProxy proxy, ITranslateApiConfig config, ITranslateApiUrlBuilder urlBuilder)
        {
            if (proxy is null) throw new ArgumentNullException(nameof(proxy));
            if (config is null) throw new ArgumentNullException(nameof(config));
            if (urlBuilder is null) throw new ArgumentNullException(nameof(urlBuilder));

            _proxy = proxy;
            _config = config;
            _urlBuilder = urlBuilder;
        }

        public async Task<string> TranslateAsync(string text)
        {
            if (String.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text));

            var url = _urlBuilder.Build(_config.Url, text);
            var response = await _proxy.TranslateAsync(url);
            var funTranslation = JsonConvert.DeserializeObject<FunTranslation>(response);

            return funTranslation?.Contents?.Translated;
        }
    }
}
