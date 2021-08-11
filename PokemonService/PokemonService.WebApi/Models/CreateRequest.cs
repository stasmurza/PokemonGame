using System.ComponentModel.DataAnnotations;

namespace PokemonService.WebApi.Models
{
    public class CreateRequest
    {
        [Required(ErrorMessage= "{0} is required")]
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(500, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Habitat { get; set; }

        public bool IsLegendary { get; set; }
    }
}
