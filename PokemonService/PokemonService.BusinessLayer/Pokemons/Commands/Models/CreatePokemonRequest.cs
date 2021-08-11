using MediatR;
using PokemonService.BusinessLayer.Pokemons.DTO;

namespace PokemonService.BusinessLayer.Pokemons.Commands.Models
{
    public class CreatePokemonRequest: IRequest<CreatePokemonResponse>
    {
        public PokemonDto Pokemon;
    }
}
