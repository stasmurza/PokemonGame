using System;

namespace PokemonService.DataAccess.Exceptions
{
    public class UniqueIndexViolationException : Exception
    {
        public UniqueIndexViolationException(string message) : base(message) {}
    }
}
