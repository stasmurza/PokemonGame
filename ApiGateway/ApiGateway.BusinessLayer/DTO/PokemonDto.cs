using System;

namespace ApiGateway.BusinessLayer.DTO
{
    public class PokemonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
