using Microsoft.Extensions.Configuration;

namespace ApiGateway.BusinessLayer.Client.Config
{
    public class ApiClientConfig : IApiClientConfig
    {
        private const string ConfigurationSection = "ApiClient";
        public int Timeout { get; set; }

        public ApiClientConfig() { }

        public ApiClientConfig(IConfiguration config)
        {
            config.GetSection(ConfigurationSection).Bind(this);
        }
    }
}
