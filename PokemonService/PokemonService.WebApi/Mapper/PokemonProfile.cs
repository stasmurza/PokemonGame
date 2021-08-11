using AutoMapper;
using PokemonService.BusinessLayer.Pokemons.DTO;
using PokemonService.DataAccess.Entities;
using PokemonService.WebApi.Models;

namespace PokemonService.WebApi.Mapper
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<CreateRequest, PokemonDto>();
            CreateMap<UpdateRequest, PokemonDto>();
            CreateMap<PokemonDto, PokemonResponse>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
