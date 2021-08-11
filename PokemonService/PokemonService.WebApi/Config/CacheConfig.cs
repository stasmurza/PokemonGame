using Microsoft.Extensions.Configuration;

namespace PokemonService.WebApi.Config
{
	public class CacheConfig : ICacheConfig
	{
		public int AbsoluteExpiration { get; set; }
		public int SlidingExpiration { get; set; }

		public CacheConfig(IConfiguration config)
		{
			config.GetSection("Cache").Bind(this);
		}
	}
}
