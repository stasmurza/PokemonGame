using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.Exceptions;
using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.TranslateApi.Factory;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.ApiComposition
{
    public class ApiComposer : IApiComposer
    {
        private readonly IPokemonService _pokemonService;
        private readonly ITranslateServiceFactory _factory;
        private readonly ILogger<ApiComposer> _logger;

        public ApiComposer(IPokemonService pokemonService, ITranslateServiceFactory factory, ILogger<ApiComposer> logger)
        {
            _pokemonService = pokemonService;
            _factory = factory;
            _logger = logger;
        }

        public async Task<PokemonDto> ComposeAsync(string name)
        {
            var pokemon = await _pokemonService.GetPokemonByNameAsync(name);
            if (pokemon is null || String.IsNullOrEmpty(pokemon.Description)) return pokemon;

            try
            {
                var translateservice = _factory.CreateService(pokemon);
                var translation = await translateservice.TranslateAsync(pokemon.Description);
                if (String.IsNullOrEmpty(translation)) throw new Exception("Translation is empty.");
                pokemon.Description = translation;
            }
            catch (ApiException ex)
            {
                _logger.LogError("Unsuccessful attempt of translation.", ex);
            }
            catch (ApiBadRequestException ex)
            {
                _logger.LogError("Unsuccessful attempt of translation.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unhandled exception: " + ex.Message, ex);
            }

            return pokemon;
        }
    }
}
