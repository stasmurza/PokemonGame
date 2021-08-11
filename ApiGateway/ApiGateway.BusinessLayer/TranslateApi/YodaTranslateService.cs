
using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;

namespace ApiGateway.BusinessLayer.TranslateApi
{
    public class YodaTranslateService : TranslateService
    {
        public YodaTranslateService(
            ITranslateServiceProxy proxy,
            IYodaApiConfig config,
            ITranslateApiUrlBuilder urlBuilder) : base(proxy, config, urlBuilder)
        {

        }
    }
}
