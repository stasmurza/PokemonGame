using ApiGateway.BusinessLayer.Client;
using ApiGateway.BusinessLayer.TranslateApi.Proxy;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ApiGateway.BusinessLayer.UnitTests.TranslateApi.Proxy
{
    [TestFixture]
    public class TranslateServiceProxyTests
    {
        [Test]
        [TestCase("https://api")]
        public async Task TranslateAsync_CorrectArguments_ApiClientReceiveCall(string url)
        {
            //Arrange
            var apiClient = Substitute.For<IApiClient>();
            apiClient.GetAsync(Arg.Any<string>()).Returns(String.Empty);
            var logger = Substitute.For<ILogger<ITranslateServiceProxy>>();
            var proxy = new TranslateServiceProxy(apiClient, logger);

            //Act
            await proxy.TranslateAsync(url);

            //Assert
            await apiClient.ReceivedWithAnyArgs().GetAsync(Arg.Any<string>());
        }

        [TestCase("test")]
        public async Task TranslateAsync_CorrectArguments_ApiClientReceiveCallWithCorrectArgument(string url)
        {
            //Arrange
            var apiClient = Substitute.For<IApiClient>();
            apiClient.GetAsync(Arg.Any<string>()).Returns(String.Empty);
            var logger = Substitute.For<ILogger<ITranslateServiceProxy>>();
            var proxy = new TranslateServiceProxy(apiClient, logger);

            //Act
            await proxy.TranslateAsync(url);

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
            var logger = Substitute.For<ILogger<ITranslateServiceProxy>>();
            var proxy = new TranslateServiceProxy(apiClient, logger);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await proxy.TranslateAsync(url));
        }

        [TestCase("test")]
        public void GetPokemonByNameAsync_ApiClientThrowException_LoggerReceiveCall(string url)
        {
            //Arrange
            var exception = new Exception();
            var apiClient = Substitute.For<IApiClient>();
            apiClient
                .When(x => x.GetAsync(Arg.Any<string>()))
                .Do(x => { throw new Exception(); });

            var logger = Substitute.For<ILogger<ITranslateServiceProxy>>();
            var proxy = new TranslateServiceProxy(apiClient, logger);

            //Act
            //Assert
            Assert.ThrowsAsync<Exception>(async () => await proxy.TranslateAsync(url));
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
                .Do(x => { throw new Exception(); });

            var logger = Substitute.For<ILogger<ITranslateServiceProxy>>();
            var proxy = new TranslateServiceProxy(apiClient, logger);

            //Act
            //Assert
            Assert.ThrowsAsync<Exception>(async () => await proxy.TranslateAsync(url));
            logger.ReceivedWithAnyArgs().LogError(exception, "Translate API call failed");
        }
    }
}
