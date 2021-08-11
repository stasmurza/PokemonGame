using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PokemonService.BusinessLayer.Pokemons.Commands.Models;
using PokemonService.BusinessLayer.Pokemons.DTO;
using PokemonService.BusinessLayer.Pokemons.Queries.Models;
using PokemonService.WebApi.Config;
using PokemonService.WebApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PokemonService.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICacheConfig _cacheConfig;

        public PokemonController(IMediator mediator, IMapper mapper, IMemoryCache memoryCache, ICacheConfig cacheConfig)
        {
            _mediator = mediator;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig;
        }

        private MemoryCacheEntryOptions GetCacheOptions()
        {
            return new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(_cacheConfig.AbsoluteExpiration),
                SlidingExpiration = TimeSpan.FromSeconds(_cacheConfig.SlidingExpiration),
            };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonResponse>> Get(Guid id)
        {
            if (_memoryCache.TryGetValue(id, out PokemonResponse cache)) return cache;

            var command = new GetPokemonRequest
            {
                Id = id
            };
            var response = await _mediator.Send(command);
            var pokemon = _mapper.Map<PokemonResponse>(response.Pokemon);

            if (pokemon is null) return NoContent();

            _memoryCache.Set(pokemon.Name, pokemon, GetCacheOptions());

            return Ok(pokemon);
        }

        [HttpGet("byname/{name}")]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonResponse>> GetByName(
            [Required]
            [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
            string name)
        {
            if (_memoryCache.TryGetValue(name, out PokemonResponse cache)) return cache;

            var command = new GetPokemonByNameRequest
            {
                Name = name
            };
            var response = await _mediator.Send(command);
            var pokemon = _mapper.Map<PokemonResponse>(response.Pokemon);

            if (pokemon is null) return NoContent();

            _memoryCache.Set(pokemon.Name, pokemon, GetCacheOptions());

            return pokemon;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonResponse>> Create(CreateRequest createRequest)
        {
            var command = new CreatePokemonRequest
            {
                Pokemon = _mapper.Map<PokemonDto>(createRequest)
            };

            var response = await _mediator.Send(command);
            var payload = _mapper.Map<PokemonResponse>(response.Pokemon);
            var url = $"{Request.GetEncodedUrl()}/{payload.Id}";

            _memoryCache.Set(payload.Id, payload, GetCacheOptions());
            _memoryCache.Set(payload.Name, payload, GetCacheOptions());

            return Created(url, payload);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(UpdateRequest updateRequest)
        {
            var command = new UpdatePokemonRequest
            {
                Pokemon = _mapper.Map<PokemonDto>(updateRequest)
            };

            var response = await _mediator.Send(command);
            var payload = _mapper.Map<PokemonResponse>(response.Pockemon);
            var url = $"{Request.GetEncodedUrl()}/{payload.Id}";

            _memoryCache.Set(payload.Id, payload, GetCacheOptions());
            _memoryCache.Set(payload.Name, payload, GetCacheOptions());

            return Created(url, payload);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePokemonRequest
            {
                Id = id
            };
            await _mediator.Send(command);

            if (_memoryCache.TryGetValue(id, out PokemonResponse cache))
            {
                _memoryCache.Remove(cache.Id);
                _memoryCache.Remove(cache.Name);
            }

            return Ok();
        }
    }
}
