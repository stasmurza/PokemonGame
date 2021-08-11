
using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;

namespace ApiGateway.BusinessLayer.TranslateApi
{
    public class ShakespeareTranslateService : TranslateService
    {
        public ShakespeareTranslateService(
            ITranslateServiceProxy proxy,
            IShakespeareApiConfig config,
            ITranslateApiUrlBuilder urlBuilder) : base(proxy, config, urlBuilder)
        {

        }
    }
}
