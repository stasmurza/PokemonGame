using ApiGateway.BusinessLayer.ApiComposition;
using ApiGateway.BusinessLayer.Client;
using ApiGateway.BusinessLayer.Client.Config;
using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.PokemonApi.Config;
using ApiGateway.BusinessLayer.PokemonApi.Proxy;
using ApiGateway.BusinessLayer.PokemonApi.UrlBuilder;
using ApiGateway.BusinessLayer.TranslateApi;
using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Factory;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;
using ApiGateway.WebApi.Config;
using ApiGateway.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace ApiGateway.WebApi
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
            

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(typeof(PokemonDto).GetTypeInfo().Assembly);

            services.AddHttpClient<ApiClient>();
            services.AddScoped<IApiClient, ApiClient>();
            services.AddScoped<IApiClientConfig, ApiClientConfig>();

            AddTranslateServiceDependencies(services);
            AddPokemonServiceDependencies(services);

            services.AddScoped<IApiComposer, ApiComposer>();

            var swaggerConfig = new SwaggerConfig(Configuration);
            if (swaggerConfig.IsEnabled)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "API gateway",
                        Version = "v1",
                        Description = "API gateway for Pokemon API, Translation API",
                    });
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var swaggerConfig = new SwaggerConfig(Configuration);
            if (swaggerConfig.IsEnabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon game API V1");
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
                        await context.Response.WriteAsync("Pokemon game API");
                    });
                });
            }
        }

        private void AddTranslateServiceDependencies(IServiceCollection services)
        {
            services.AddScoped<ITranslateApiUrlBuilder, TranslateApiUrlBuilder>();
            services.AddScoped<IShakespeareApiConfig, ShakespeareApiConfig>();
            services.AddScoped<IYodaApiConfig, YodaApiConfig>();
            services.AddScoped<ITranslateServiceProxy, TranslateServiceProxy>();
            services.AddScoped<ITranslateServiceFactory, TranslateServiceFactory>();
        }

        private void AddPokemonServiceDependencies(IServiceCollection services)
        {
            services.AddScoped<IPokemonApiUrlBuilder, PokemonApiUrlBuilder>();
            services.AddScoped<IPokemonApiConfig, PokemonApiConfig>();
            services.AddScoped<IPokemonServiceProxy, PokemonServiceProxy>();
            services.AddScoped<IPokemonService, PokemonService>();
        }
    }
}
