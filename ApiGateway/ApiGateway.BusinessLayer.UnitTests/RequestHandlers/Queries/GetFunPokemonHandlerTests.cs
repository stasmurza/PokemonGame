using ApiGateway.BusinessLayer.ApiComposition;
using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.RequestHandlers.Queries;
using ApiGateway.BusinessLayer.RequestHandlers.Queries.Models;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.RequestHandlers.Queries
{
    [TestFixture]
    public class GetFunPokemonHandlerTests
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
        public async Task Handle_CorrectRequest_ApiComposerReceiveCall()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var apiComposer = Substitute.For<IApiComposer>();
            apiComposer.ComposeAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetFunPokemonHandler(apiComposer);
            var request = new GetFunPokemonRequest()
            {
                Name = "test"
            };

            //Act
            await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            await apiComposer.ReceivedWithAnyArgs().ComposeAsync(Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task Handle_CorrectRequest_ApiComposerReceiveCallWithCorrectArgument(string name)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var apiComposer = Substitute.For<IApiComposer>();
            apiComposer.ComposeAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetFunPokemonHandler(apiComposer);
            var request = new GetFunPokemonRequest()
            {
                Name = name
            };

            //Act
            await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            await apiComposer.ReceivedWithAnyArgs().ComposeAsync(name);
        }

        [TestCase("test")]
        public async Task Handle_CorrectRequest_ReturnsApiComposerResponse(string name)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var apiComposer = Substitute.For<IApiComposer>();
            apiComposer.ComposeAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetFunPokemonHandler(apiComposer);
            var request = new GetFunPokemonRequest()
            {
                Name = name
            };

            //Act
            var result = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.That(result.Pokemon, Is.EqualTo(pokemon));
        }

        [Test]
        public void Handle_EmptyRequest_ThrowException()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var apiComposer = Substitute.For<IApiComposer>();
            apiComposer.ComposeAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetFunPokemonHandler(apiComposer);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(null, new System.Threading.CancellationToken()));
        }
    }
}
