using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.PokemonApi.Proxy
{
    public interface IPokemonServiceProxy
    {
        public Task<string> GetPokemonByNameAsync(string url);
    }
}
