using ApiGateway.BusinessLayer.DTO;
using ApiGateway.WebApi.Models;
using AutoMapper;

namespace ApiGateway.WebApi.Mapper
{
    public class PokemonProfile : Profile
    {
        public PokemonProfile()
        {
            CreateMap<PokemonDto, PokemonResponse>();
        }
    }
}
