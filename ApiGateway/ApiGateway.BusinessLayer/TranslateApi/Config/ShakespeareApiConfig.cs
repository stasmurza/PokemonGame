using Microsoft.Extensions.Configuration;

namespace ApiGateway.BusinessLayer.TranslateApi.Config
{
    public class ShakespeareApiConfig : IShakespeareApiConfig
    {
        private const string ConfigurationSection = "ShakespeareApi";
        public string Url { get; set; }

        public ShakespeareApiConfig() { }

        public ShakespeareApiConfig(IConfiguration config)
        {
            config.GetSection(ConfigurationSection).Bind(this);
        }
    }
}
