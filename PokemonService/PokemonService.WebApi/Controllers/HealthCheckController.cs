using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PokemonService.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [Produces("application/json")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
