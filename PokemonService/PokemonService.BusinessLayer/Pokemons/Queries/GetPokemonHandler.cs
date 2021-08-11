using AutoMapper;
using MediatR;
using PokemonService.BusinessLayer.Pokemons.DTO;
using PokemonService.BusinessLayer.Pokemons.Queries.Models;
using PokemonService.DataAccess.Repositiories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonService.BusinessLayer.Pokemons.Queries
{
    public class GetPokemonHandler: IRequestHandler<GetPokemonRequest, GetPokemonResponse>
    {
        private readonly IPokemonRepository _repository;
        private readonly IMapper _mapper;

        public GetPokemonHandler(IPokemonRepository repository, IMapper mapper)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetPokemonResponse> Handle(GetPokemonRequest request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var pokemon = await _repository.GetPokemonAsync(request.Id);

            return new GetPokemonResponse
            {
                Pokemon = _mapper.Map<PokemonDto>(pokemon)
            };
        }
    }
}
