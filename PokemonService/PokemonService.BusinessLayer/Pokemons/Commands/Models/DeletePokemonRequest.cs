using MediatR;
using System;

namespace PokemonService.BusinessLayer.Pokemons.Commands.Models
{
    public class DeletePokemonRequest : IRequest
    {
        public Guid Id;
    }
}
