using System;
using System.ComponentModel.DataAnnotations;

namespace PokemonService.WebApi.Models
{
    public class UpdateRequest : CreateRequest
    {
        public Guid Id { get; set; }
    }
}
