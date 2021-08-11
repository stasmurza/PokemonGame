using AutoMapper;
using MediatR;
using PokemonService.BusinessLayer.Pokemons.Commands.Models;
using PokemonService.BusinessLayer.Pokemons.DTO;
using PokemonService.DataAccess.Entities;
using PokemonService.DataAccess.Repositiories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonService.BusinessLayer.Pokemons.Commands
{
    public class CreatePokemonHandler: IRequestHandler<CreatePokemonRequest, CreatePokemonResponse>
    {
        private IPokemonRepository _repository;
        private readonly IMapper _mapper;

        public CreatePokemonHandler(IPokemonRepository repository, IMapper mapper)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreatePokemonResponse> Handle(CreatePokemonRequest request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var pokemon = _mapper.Map<Pokemon>(request.Pokemon);
            await _repository.CreatePokemonAsync(pokemon);

            return new CreatePokemonResponse
            {
                Pokemon = _mapper.Map<PokemonDto>(pokemon)
            };
        }
    }
}
