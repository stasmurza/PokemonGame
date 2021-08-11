using ApiGateway.BusinessLayer.ApiComposition;
using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.Exceptions;
using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.TranslateApi;
using ApiGateway.BusinessLayer.TranslateApi.Factory;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.UnitTests.ApiComposition
{
    public class ApiComposerTests
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
        [TestCase("test")]
        public async Task ComposeAsync_PokemonServiceReturnNull_ReturnNull(string name)
        {
            //Arrange
            var pokemon = (PokemonDto)null;
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns((PokemonDto)null);

            var translateService = Substitute.For<ITranslateService>();

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            Assert.That(result, Is.EqualTo(pokemon));
        }

        [Test]
        [TestCase("test", "test description")]
        public async Task ComposeAsync_TranslateServiceReturnTranslarion_ReturnCorrectDescription(string name, string description)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);

            var translateService = Substitute.For<ITranslateService>();
            translateService.TranslateAsync(Arg.Any<string>()).Returns(description);

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            Assert.That(result.Description, Is.EqualTo(description));
        }

        [Test]
        [TestCase("test", null)]
        public async Task ComposeAsync_TranslateServiceReturnNull_LoggerReceiveCall(string name, string description)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);

            var translateService = Substitute.For<ITranslateService>();
            translateService.TranslateAsync(Arg.Any<string>()).Returns(description);

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            logger.ReceivedWithAnyArgs().LogError(new Exception(), String.Empty);
        }

        [Test]
        [TestCase("test", "test description")]
        public async Task ComposeAsync_TranslateServiceThrowApiException_LoggerReceiveCallWithCorrectArgument(string name, string description)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);

            var exception = new ApiException(String.Empty);
            var translateService = Substitute.For<ITranslateService>();
            translateService
                .When(x => x.TranslateAsync(Arg.Any<string>()))
                .Do(x => { throw exception; });

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            logger.ReceivedWithAnyArgs().LogError("Unsuccessful attempt of translation.", exception);
        }

        [Test]
        [TestCase("test", "test description")]
        public async Task ComposeAsync_TranslateServiceThrowApiBadRequestException_LoggerReceiveCallWithCorrectArgument(string name, string description)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);

            var exception = new ApiBadRequestException(String.Empty);
            var translateService = Substitute.For<ITranslateService>();
            translateService
                .When(x => x.TranslateAsync(Arg.Any<string>()))
                .Do(x => { throw exception; });

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            logger.ReceivedWithAnyArgs().LogError("Unsuccessful attempt of translation.", exception);
        }

        [Test]
        [TestCase("test", "test description")]
        public async Task ComposeAsync_TranslateServiceThrowException_LoggerReceiveCallWithCorrectArgument(string name, string description)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);

            var exception = new Exception(String.Empty);
            var translateService = Substitute.For<ITranslateService>();
            translateService
                .When(x => x.TranslateAsync(Arg.Any<string>()))
                .Do(x => { throw exception; });

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            logger.ReceivedWithAnyArgs().LogError("Unhandled exception: " + exception.Message, exception);
        }

        [Test]
        [TestCase("test", "test description")]
        public async Task ComposeAsync_TranslateServiceThrowException_ReturnCorrectDescription(string name, string description)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            pokemon.Description = description;
            var pokemonService = Substitute.For<IPokemonService>();
            pokemonService.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);

            var exception = new Exception(String.Empty);
            var translateService = Substitute.For<ITranslateService>();
            translateService
                .When(x => x.TranslateAsync(Arg.Any<string>()))
                .Do(x => { throw exception; });

            var factory = Substitute.For<ITranslateServiceFactory>();
            factory.CreateService(pokemon).Returns(translateService);
            var logger = Substitute.For<ILogger<ApiComposer>>();

            var apiComposer = new ApiComposer(pokemonService, factory, logger);

            //Act
            var result = await apiComposer.ComposeAsync(name);

            //Assert
            Assert.That(result.Description, Is.EqualTo(description));
        }
    }
}
