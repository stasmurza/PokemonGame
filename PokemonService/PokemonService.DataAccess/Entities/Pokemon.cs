using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonService.DataAccess.Entities
{
    public class Pokemon
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
