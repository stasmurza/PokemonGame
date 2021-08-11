using ApiGateway.BusinessLayer.DTO;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.ApiComposition
{
    public interface IApiComposer
    {
        public Task<PokemonDto> ComposeAsync(string name);
    }
}
