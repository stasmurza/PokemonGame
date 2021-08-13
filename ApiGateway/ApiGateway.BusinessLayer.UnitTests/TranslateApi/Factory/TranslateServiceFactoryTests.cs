using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.TranslateApi;
using ApiGateway.BusinessLayer.TranslateApi.Config;
using ApiGateway.BusinessLayer.TranslateApi.Factory;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using System;

namespace ApiGateway.BusinessLayer.UnitTests.TranslateApi.Factory
{
    [TestFixture]
    public class TranslateServiceFactoryTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture()
            .Customize(new AutoNSubstituteCustomization()
            {
                ConfigureMembers = true
            });
        }

        [Test]
        public void CreateService_CorrectArgument_CreateShakespeareTranslateServiceInstance()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.IsLegendary = false;
            pokemon.Habitat = "test";
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var yodaApiConfig = Substitute.For<IYodaApiConfig>();
            var shakespeareApiConfig = Substitute.For<IShakespeareApiConfig>();

            var factory = new TranslateServiceFactory(proxy, shakespeareApiConfig, yodaApiConfig, urlBuilder);

            //Act
            var service = factory.CreateService(pokemon);

            //Assert
            Assert.That(service, Is.TypeOf<ShakespeareTranslateService>());
        }

        [Test]
        public void CreateService_IsLegendaryPokemon_CreateYodaTranslateServiceInstance()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.IsLegendary = true;
            pokemon.Habitat = "test";
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var yodaApiConfig = Substitute.For<IYodaApiConfig>();
            var shakespeareApiConfig = Substitute.For<IShakespeareApiConfig>();

            var factory = new TranslateServiceFactory(proxy, shakespeareApiConfig, yodaApiConfig, urlBuilder);

            //Act
            var service = factory.CreateService(pokemon);

            //Assert
            Assert.That(service, Is.TypeOf<YodaTranslateService>());
        }

        [Test]
        public void CreateService_PokemonWithSpecialHabitat_CreateYodaTranslateServiceInstance()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.IsLegendary = false;
            pokemon.Habitat = "cave";
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var yodaApiConfig = Substitute.For<IYodaApiConfig>();
            var shakespeareApiConfig = Substitute.For<IShakespeareApiConfig>();

            var factory = new TranslateServiceFactory(proxy, shakespeareApiConfig, yodaApiConfig, urlBuilder);

            //Act
            var service = factory.CreateService(pokemon);

            //Assert
            Assert.That(service, Is.TypeOf<YodaTranslateService>());
        }

        [Test]
        public void CreateService_PokemonDtoIsNull_ThrowException()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var proxy = Substitute.For<ITranslateServiceProxy>();
            proxy.TranslateAsync(Arg.Any<string>()).Returns(String.Empty);
            var urlBuilder = Substitute.For<ITranslateApiUrlBuilder>();
            var yodaApiConfig = Substitute.For<IYodaApiConfig>();
            var shakespeareApiConfig = Substitute.For<IShakespeareApiConfig>();

            var factory = new TranslateServiceFactory(proxy, shakespeareApiConfig, yodaApiConfig, urlBuilder);

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => factory.CreateService(null));
        }
    }
}
