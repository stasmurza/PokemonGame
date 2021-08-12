using Microsoft.EntityFrameworkCore;
using PokemonService.DataAccess.Entities;
using PokemonService.DataAccess.Exceptions;
using System;
using System.Threading.Tasks;

namespace PokemonService.DataAccess.Repositiories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ApiContext _dbContext;

        public PokemonRepository(ApiContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            _dbContext = context;
        }

        public Task<Pokemon> GetPokemonAsync(Guid id)
        {
            return _dbContext.Pokemons.FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return _dbContext.Pokemons.FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task CreatePokemonAsync(Pokemon pokemon)
        {
            if (pokemon is null) throw new ArgumentNullException(nameof(pokemon));

            var exist = await _dbContext.Pokemons.AnyAsync(i => i.Name.Equals(pokemon.Name));
            if (exist) throw new UniqueIndexViolationException($"Entity with name '{pokemon.Name}' is already exist");

            await _dbContext.Pokemons.AddAsync(pokemon);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            if (pokemon is null) throw new ArgumentNullException(nameof(pokemon));

            var exist = await _dbContext.Pokemons.AnyAsync(i => i.Id == pokemon.Id);
            if (!exist) throw new EntityNotFoundException($"Entity with id '{pokemon.Id}' has not found");

            _dbContext.Attach(pokemon);
            _dbContext.Entry(pokemon).Property(p => p.Name).IsModified = true;
            _dbContext.Entry(pokemon).Property(p => p.Description).IsModified = true;
            _dbContext.Entry(pokemon).Property(p => p.Habitat).IsModified = true;
            _dbContext.Entry(pokemon).Property(p => p.IsLegendary).IsModified = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePokemonAsync(Guid id)
        {
            var exist = await _dbContext.Pokemons.AnyAsync(i => i.Id == id);
            if (!exist) throw new EntityNotFoundException($"Entity with id {id} has not found");

            var entity = new Pokemon() { Id = id };
            _dbContext.Pokemons.Attach(entity);
            _dbContext.Pokemons.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
