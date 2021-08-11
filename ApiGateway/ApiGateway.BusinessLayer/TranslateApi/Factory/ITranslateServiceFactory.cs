using ApiGateway.BusinessLayer.DTO;

namespace ApiGateway.BusinessLayer.TranslateApi.Factory
{
    public interface ITranslateServiceFactory
    {
        public ITranslateService CreateService(PokemonDto pokemon);
    }
}
