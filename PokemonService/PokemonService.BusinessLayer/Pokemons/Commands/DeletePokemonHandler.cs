using MediatR;
using PokemonService.BusinessLayer.Pokemons.Commands.Models;
using PokemonService.DataAccess.Repositiories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PokemonService.BusinessLayer.Pokemons.Commands
{
    public class DeletePokemonHandler : IRequestHandler<DeletePokemonRequest>
    {
        private IPokemonRepository _repository;

        public DeletePokemonHandler(IPokemonRepository repository)
        {
            if (repository is null) throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }

        public async Task<Unit> Handle(DeletePokemonRequest request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            await _repository.DeletePokemonAsync(request.Id);

            //log
            return Unit.Value;
        }
    }
}
