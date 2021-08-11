using Microsoft.Extensions.Configuration;

namespace ApiGateway.WebApi.Config
{
	public class SwaggerConfig : ISwaggerConfig
	{
		public SwaggerConfig(IConfiguration config)
		{
			config.GetSection("Swagger").Bind(this);
		}

		public bool IsEnabled { get; set; }
	}
}
