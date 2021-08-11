using Microsoft.EntityFrameworkCore;
using PokemonService.DataAccess.Entities;

namespace PokemonService.DataAccess
{
    public class ApiContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasIndex(b => b.Name)
                .IsUnique();
        }
    }
}
