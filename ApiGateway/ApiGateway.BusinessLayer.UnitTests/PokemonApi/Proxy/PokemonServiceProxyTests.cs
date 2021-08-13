using ApiGateway.BusinessLayer.Client;
using ApiGateway.BusinessLayer.PokemonApi.Proxy;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.PokemonApi.Proxy
{
    [TestFixture]
    public class PokemonServiceProxyTests
    {
        [TestCase("https://api")]
        public async Task GetPokemonByNameAsync_CorrectParameters_ApiClientReceiveCall(string url)
        {
            //Arrange
            var apiClient = Substitute.For<IApiClient>();
            apiClient.GetAsync(Arg.Any<string>()).Returns(String.Empty);
            var logger = Substitute.For<ILogger<IPokemonServiceProxy>>();
            var proxy = new PokemonServiceProxy(apiClient, logger);

            //Act
            await proxy.GetPokemonByNameAsync(url);

            //Assert
            await apiClient.ReceivedWithAnyArgs().GetAsync(Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectParameters_ApiClientReceiveCallWithCorrectArgument(string url)
        {
            //Arrange
            var apiClient = Substitute.For<IApiClient>();
            apiClient.GetAsync(url).Returns(String.Empty);
            var logger = Substitute.For<ILogger<IPokemonServiceProxy>>();
            var proxy = new PokemonServiceProxy(apiClient, logger);

            //Act
            await proxy.GetPokemonByNameAsync(url);

            //Assert
            await apiClient.Received().GetAsync(url);
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetPokemonByNameAsync_EmptyUrl_ThrowException(string url)
        {
            //Arrange
            var apiClient = Substitute.For<IApiClient>();
            apiClient.GetAsync(Arg.Any<string>()).Returns(String.Empty);
            var logger = Substitute.For<ILogger<IPokemonServiceProxy>>();
            var proxy = new PokemonServiceProxy(apiClient, logger);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async() => await proxy.GetPokemonByNameAsync(url));
        }

        [TestCase("test")]
        public void GetPokemonByNameAsync_ApiClientThrowException_LoggerReceiveCall(string url)
        {
            //Arrange
            var exception = new Exception();
            var apiClient = Substitute.For<IApiClient>();
            apiClient
                .When(x => x.GetAsync(Arg.Any<string>()))
                .Do(x => { throw exception; });

            var logger = Substitute.For<ILogger<IPokemonServiceProxy>>();
            var proxy = new PokemonServiceProxy(apiClient, logger);

            //Assert
            Assert.ThrowsAsync<Exception>(async () => await proxy.GetPokemonByNameAsync(url));
            logger.ReceivedWithAnyArgs().LogError(exception, String.Empty);
        }

        [TestCase("test")]
        public void GetPokemonByNameAsync_ApiClientThrowException_LoggerReceiveCallWithCorrectArguments(string url)
        {
            //Arrange
            var exception = new Exception();
            var apiClient = Substitute.For<IApiClient>();
            apiClient
                .When(x => x.GetAsync(Arg.Any<string>()))
                .Do(x => { throw exception; });

            var logger = Substitute.For<ILogger<IPokemonServiceProxy>>();
            var proxy = new PokemonServiceProxy(apiClient, logger);

            //Assert
            Assert.ThrowsAsync<Exception>(async () => await proxy.GetPokemonByNameAsync(url));
            logger.Received().LogError(exception, "Pokemon API call failed");
        }
    }
}
