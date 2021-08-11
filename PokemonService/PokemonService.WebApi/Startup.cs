using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PokemonService.DataAccess.Repositiories;
using PokemonService.BusinessLayer.Pokemons.DTO;
using PokemonService.WebApi.Config;
using System.Reflection;
using PokemonService.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using PokemonService.WebApi.Filters;

namespace PokemonService.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                config.Filters.Add<ExceptionFilter>();
                config.Filters.Add<ActivityLoggerFilter>();
            });
            var swaggerConfig = new SwaggerConfig(Configuration);
            if (swaggerConfig.IsEnabled)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Pokemon API",
                        Version = "v1",
                        Description = "Pokemon CRUD API",
                    });
                });
            }

            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(PokemonDto).GetTypeInfo().Assembly);
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "Pokemons"));
            services.AddScoped<ICacheConfig, CacheConfig>();
            services.AddMemoryCache();
        }

        //This method gets called by the runtime.Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerConfig = new SwaggerConfig(Configuration);
            if (swaggerConfig.IsEnabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credential

            if (swaggerConfig.IsEnabled)
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            else
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("Pokemon API");
                    });
                });
            }
        }
    }
}
