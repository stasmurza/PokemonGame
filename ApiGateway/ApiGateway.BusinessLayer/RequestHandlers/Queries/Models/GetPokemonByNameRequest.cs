using MediatR;

namespace ApiGateway.BusinessLayer.RequestHandlers.Queries.Models
{
    public class GetPokemonByNameRequest : IRequest<GetPokemonResponse>
    {
        public string Name;
    }
}
