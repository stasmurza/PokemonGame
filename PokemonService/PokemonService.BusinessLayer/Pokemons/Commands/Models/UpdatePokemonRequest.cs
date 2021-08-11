using MediatR;
using PokemonService.BusinessLayer.Pokemons.DTO;

namespace PokemonService.BusinessLayer.Pokemons.Commands.Models
{
    public class UpdatePokemonRequest : IRequest<UpdatePokemonResponse>
    {
        public PokemonDto Pokemon;
    }
}
