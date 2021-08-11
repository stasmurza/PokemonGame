
namespace PokemonService.WebApi.Config
{
	public interface ICacheConfig
	{
		public int AbsoluteExpiration { get; set; }
		public int SlidingExpiration { get; set; }
	}
}
