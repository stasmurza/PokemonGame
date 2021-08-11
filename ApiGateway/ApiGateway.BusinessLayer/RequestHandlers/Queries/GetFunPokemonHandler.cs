using ApiGateway.BusinessLayer.ApiComposition;
using ApiGateway.BusinessLayer.RequestHandlers.Queries.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.RequestHandlers.Queries
{
    public class GetFunPokemonHandler : IRequestHandler<GetFunPokemonRequest, GetPokemonResponse>
    {
        private readonly IApiComposer _apiComposer;

        public GetFunPokemonHandler(IApiComposer apiComposer)
        {
            if (apiComposer is null) throw new ArgumentNullException(nameof(apiComposer));

            _apiComposer = apiComposer;
        }

        public async Task<GetPokemonResponse> Handle(GetFunPokemonRequest request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var pokemon = await _apiComposer.ComposeAsync(request.Name);

            return new GetPokemonResponse
            {
                Pokemon = pokemon
            };
        }
    }
}
