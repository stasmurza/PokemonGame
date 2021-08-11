using Microsoft.Extensions.Configuration;

namespace ApiGateway.BusinessLayer.TranslateApi.Config
{
    public class YodaApiConfig : IYodaApiConfig
    {
        private const string ConfigurationSection = "YodaApi";
        public string Url { get; set; }

        public YodaApiConfig() { }

        public YodaApiConfig(IConfiguration config)
        {
            config.GetSection(ConfigurationSection).Bind(this);
        }
    }
}
