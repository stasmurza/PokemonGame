using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.PokemonApi.Config;
using ApiGateway.BusinessLayer.PokemonApi.Proxy;
using ApiGateway.BusinessLayer.PokemonApi.UrlBuilder;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.PokemonApi
{
    [TestFixture]
    public class PokemonServiceTests
    {
        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_UrlBuilderReceiveCall(string name)
        {
            //Arrange
            var proxy = Substitute.For<IPokemonServiceProxy>();
            proxy.GetPokemonByNameAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<IPokemonApiUrlBuilder>();
            var config = Substitute.For<IPokemonApiConfig>();

            var service = new PokemonService(proxy, config, urlBuilder);

            //Act
            await service.GetPokemonByNameAsync(name);

            //Assert
            urlBuilder.ReceivedWithAnyArgs().ByNameUrl(Arg.Any<string>(), Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_UrlBuilderReceiveCorrectArgument(string name)
        {
            //Arrange
            var proxy = Substitute.For<IPokemonServiceProxy>();
            proxy.GetPokemonByNameAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<IPokemonApiUrlBuilder>();
            var config = Substitute.For<IPokemonApiConfig>();

            var service = new PokemonService(proxy, config, urlBuilder);

            //Act
            await service.GetPokemonByNameAsync(name);

            //Assert
            urlBuilder.ReceivedWithAnyArgs().ByNameUrl(Arg.Any<string>(), Arg.Is<string>(i => i.Equals(name)));
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_ProxyReceiveCall(string name)
        {
            //Arrange
            var proxy = Substitute.For<IPokemonServiceProxy>();
            proxy.GetPokemonByNameAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<IPokemonApiUrlBuilder>();
            var config = Substitute.For<IPokemonApiConfig>();

            var service = new PokemonService(proxy, config, urlBuilder);

            //Act
            await service.GetPokemonByNameAsync(name);

            //Assert
            await proxy.ReceivedWithAnyArgs().GetPokemonByNameAsync(Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_ProxyReceiveCorrectArgument(string name)
        {
            //Arrange
            var proxy = Substitute.For<IPokemonServiceProxy>();
            proxy.GetPokemonByNameAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<IPokemonApiUrlBuilder>();
            var config = Substitute.For<IPokemonApiConfig>();

            var service = new PokemonService(proxy, config, urlBuilder);

            //Act
            await service.GetPokemonByNameAsync(name);

            //Assert
            await proxy.ReceivedWithAnyArgs().GetPokemonByNameAsync(Arg.Is<string>(i => i.Equals(name)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetPokemonByNameAsync_EmptyName_ThrowException(string name)
        {
            //Arrange
            var proxy = Substitute.For<IPokemonServiceProxy>();
            proxy.GetPokemonByNameAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<IPokemonApiUrlBuilder>();
            var config = Substitute.For<IPokemonApiConfig>();

            var service = new PokemonService(proxy, config, urlBuilder);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.GetPokemonByNameAsync(name));
        }
    }
}
