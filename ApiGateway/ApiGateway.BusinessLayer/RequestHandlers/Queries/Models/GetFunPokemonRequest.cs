using MediatR;

namespace ApiGateway.BusinessLayer.RequestHandlers.Queries.Models
{
    public class GetFunPokemonRequest : IRequest<GetPokemonResponse>
    {
        public string Name;
    }
}
