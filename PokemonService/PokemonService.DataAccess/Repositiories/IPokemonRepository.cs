
using PokemonService.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace PokemonService.DataAccess.Repositiories
{
    public interface IPokemonRepository
    {
        Task<Pokemon> GetPokemonAsync(Guid id);
        Task<Pokemon> GetPokemonByNameAsync(string name);
        Task CreatePokemonAsync(Pokemon pokemon);
        Task UpdatePokemonAsync(Pokemon pokemon);
        Task DeletePokemonAsync(Guid id);
    }
}
