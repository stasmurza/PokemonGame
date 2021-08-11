using ApiGateway.BusinessLayer.RequestHandlers.Queries.Models;
using ApiGateway.WebApi.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ApiGateway.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PokemonController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("byname/{name}")]
        [SwaggerOperation(OperationId ="GetPokemonByName")]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ResponseCache(NoStore = true)]
        public async Task<ActionResult<PokemonResponse>> GetPokemon(
            [Required]
            [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
            string name)
        {
            var query = new GetPokemonByNameRequest { Name = name };
            var response = await _mediator.Send(query);
            var pokemon = _mapper.Map<PokemonResponse>(response.Pokemon);

            if (pokemon is null) return NoContent();

            return pokemon;
        }
        
        [HttpGet("translated/{name}")]
        [SwaggerOperation(OperationId = "GetFunPokemon")]
        [ProducesResponseType(typeof(PokemonResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ResponseCache(NoStore = true)]
        public async Task<ActionResult<PokemonResponse>> GetFunPokemon(
            [Required]
            [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
            string name)
        {
            var query = new GetFunPokemonRequest { Name = name };
            var response = await _mediator.Send(query);
            var pokemon = _mapper.Map<PokemonResponse>(response.Pokemon);

            if (pokemon is null) return NoContent();

            return pokemon;
        }
    }
}
