using MediatR;

namespace PokemonService.BusinessLayer.Pokemons.Queries.Models
{
    public class GetPokemonByNameRequest : IRequest<GetPokemonResponse> 
    {
        public string Name;
    }
}
