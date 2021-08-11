using MediatR;
using System;

namespace PokemonService.BusinessLayer.Pokemons.Queries.Models
{
    public class GetPokemonRequest : IRequest<GetPokemonResponse>
    {
        public Guid Id;
    }
}
