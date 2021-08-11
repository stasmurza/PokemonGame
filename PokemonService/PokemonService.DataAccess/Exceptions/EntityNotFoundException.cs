using System;

namespace PokemonService.DataAccess.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException(string message) : base(message) {}
    }
}
