using ApiGateway.BusinessLayer.DTO;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.PokemonApi
{
    public interface IPokemonService
    {
        Task<PokemonDto> GetPokemonByNameAsync(string name);
    }
}
