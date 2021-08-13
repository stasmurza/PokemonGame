using ApiGateway.BusinessLayer.DTO;
using ApiGateway.BusinessLayer.PokemonApi;
using ApiGateway.BusinessLayer.RequestHandlers.Queries;
using ApiGateway.BusinessLayer.RequestHandlers.Queries.Models;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.RequestHandlers.Queries
{
    [TestFixture]
    public class GetPokemonByNameHandlerTests
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
        public async Task Handle_CorrectRequest_PokemonServiceReceiveCall()
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var service = Substitute.For<IPokemonService>();
            service.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetPokemonByNameHandler(service);
            var request = new GetPokemonByNameRequest()
            {
                Name = "test"
            };

            //Act
            await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            await service.ReceivedWithAnyArgs().GetPokemonByNameAsync(Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task Handle_CorrectRequest_PokemonServiceProxyReceiveCallWithCorrectArgument(string name)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var service = Substitute.For<IPokemonService>();
            service.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetPokemonByNameHandler(service);
            var request = new GetPokemonByNameRequest()
            {
                Name = name
            };

            //Act
            await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            await service.ReceivedWithAnyArgs().GetPokemonByNameAsync(name);
        }

        [TestCase("test")]
        public async Task Handle_CorrectRequest_ReturnsPokemonServiceProxyResponse(string name)
        {
            //Arrange
            var pokemon = _fixture.Create<PokemonDto>();
            var service = Substitute.For<IPokemonService>();
            service.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetPokemonByNameHandler(service);
            var request = new GetPokemonByNameRequest()
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
            var service = Substitute.For<IPokemonService>();
            service.GetPokemonByNameAsync(Arg.Any<string>()).Returns(pokemon);
            var handler = new GetPokemonByNameHandler(service);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(null, new System.Threading.CancellationToken()));
        }
    }
}
