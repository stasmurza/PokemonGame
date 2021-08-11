using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.RequestHandlers.Queries.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.RequestHandlers.Queries
{
    public class GetPokemonByNameHandler : IRequestHandler<GetPokemonByNameRequest, GetPokemonResponse>
    {
        private readonly IPokemonService _service;

        public GetPokemonByNameHandler(IPokemonService service)
        {
            if (service is null) throw new ArgumentNullException(nameof(service));

            _service = service;
        }

        public async Task<GetPokemonResponse> Handle(GetPokemonByNameRequest request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var pokemon = await _service.GetPokemonByNameAsync(request.Name);

            return new GetPokemonResponse
            {
                Pokemon = pokemon
            };
        }
    }
}
