
using Microsoft.Extensions.Configuration;

namespace ApiGateway.BusinessLayer.PokemonApi.Config
{
    public class PokemonApiConfig : IPokemonApiConfig
    {
        private const string ConfigurationSection = "PokemonApi";
        public string Url { get; set; }

        public PokemonApiConfig() { }

        public PokemonApiConfig(IConfiguration config)
        {
            config.GetSection(ConfigurationSection).Bind(this);
        }
    }
}
