using ApiGateway.BusinessLayer.TranslateApi;
using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.TranslateApi
{
    [TestFixture]
    public class TranslateServiceTests
    {
        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_UrlBuilderReceiveCall(string name)
        {
            //Arrange
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var config = Substitute.For<ITranslateApiConfig>();

            var service = new TranslateService(proxy, config, urlBuilder);

            //Act
            await service.TranslateAsync(name);

            //Assert
            urlBuilder.ReceivedWithAnyArgs().Build(Arg.Any<string>(), Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_UrlBuilderReceiveCorrectArgument(string name)
        {
            //Arrange
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var config = Substitute.For<ITranslateApiConfig>();

            var service = new TranslateService(proxy, config, urlBuilder);

            //Act
            await service.TranslateAsync(name);

            //Assert
            urlBuilder.ReceivedWithAnyArgs().Build(Arg.Any<string>(), Arg.Is<string>(i => i.Equals(name)));
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_ProxyReceiveCall(string name)
        {
            //Arrange
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var config = Substitute.For<ITranslateApiConfig>();

            var service = new TranslateService(proxy, config, urlBuilder);

            //Act
            await service.TranslateAsync(name);

            //Assert
            await proxy.ReceivedWithAnyArgs().TranslateAsync(Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task GetPokemonByNameAsync_CorrectName_ProxyReceiveCorrectArgument(string name)
        {
            //Arrange
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var config = Substitute.For<ITranslateApiConfig>();

            var service = new TranslateService(proxy, config, urlBuilder);

            //Act
            await service.TranslateAsync(name);

            //Assert
            await proxy.ReceivedWithAnyArgs().TranslateAsync(Arg.Is<string>(i => i.Equals(name)));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetPokemonByNameAsync_EmptyName_ThrowException(string name)
        {
            //Arrange
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var config = Substitute.For<ITranslateApiConfig>();

            var service = new TranslateService(proxy, config, urlBuilder);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await service.TranslateAsync(name));
        }
    }
}
