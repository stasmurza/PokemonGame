
namespace ApiGateway.BusinessLayer.PokemonApi.UrlBuilder
{
    public interface IPokemonApiUrlBuilder
    {
        public string ByNameUrl(string apiUrl, string name);
    }
}
