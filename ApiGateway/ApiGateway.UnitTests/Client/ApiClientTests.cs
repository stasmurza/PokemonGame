using ApiGateway.BusinessLayer.Client;
using ApiGateway.BusinessLayer.Client.Config;
using ApiGateway.BusinessLayer.Exceptions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.UnitTests.Client
{
    public class ApiClientTests
    {
        private IApiClientConfig _apiConfig;

        [SetUp]
        public void Setup()
        {
            _apiConfig = new ApiClientConfig
            {
                Timeout = 30000
            };
        }

        [Test]
        [TestCase("https://api")]
        public async Task GetAsync_CorrectUrl_ApiClientReceiveCall(string url)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock();
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            await apiClient.GetAsync(url);

            //Assert
            Assert.That(messageHandler.NumberOfCalls, Is.EqualTo(1));
        }

        [Test]
        [TestCase("https://api", "", HttpStatusCode.NoContent)]
        [TestCase("https://api", "test", HttpStatusCode.NoContent)]
        public async Task GetAsync_ResponseOk_ReturnCorrectResult(string url, string response, HttpStatusCode code)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock(response, code);
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            var result = await apiClient.GetAsync(url);

            //Assert
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        [TestCase("https://api", "", HttpStatusCode.NoContent)]
        [TestCase("https://api", "test", HttpStatusCode.NoContent)]
        public async Task GetAsync_ResponseNoContent_ReturnCorrectResult(string url, string response, HttpStatusCode code)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock(response, code);
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            var result = await apiClient.GetAsync(url);

            //Assert
            Assert.That(result, Is.EqualTo(response));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void GetAsync_EmptyUrl_ThrowException(string url)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock();
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async() => await apiClient.GetAsync(url));
        }

        [Test]
        [TestCase("https://api", "", HttpStatusCode.BadRequest)]
        [TestCase("https://api", "Object not found", HttpStatusCode.BadRequest)]
        public void GetAsync_ResponseNotOk_ThrowException(string url, string response, HttpStatusCode code)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock(response, code);
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            //Assert
            Assert.ThrowsAsync<ApiBadRequestException>(async () => await apiClient.GetAsync(url));
        }

        [Test]
        [TestCase("https://api", "Object not found", HttpStatusCode.BadRequest)]
        public void GetAsync_BadRequest_CorrectExceptionMessage(string url, string responseContent, HttpStatusCode code)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock(responseContent, code);
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            //Assert
            var ex = Assert.ThrowsAsync<ApiBadRequestException>(async () => await apiClient.GetAsync(url));
            Assert.That(ex.Message, Is.EqualTo(responseContent));
        }

        [Test]
        [TestCase("https://api", "", HttpStatusCode.InternalServerError)]
        public void GetAsync_UnknowStatusCode_ThrowException(string url, string responseContent, HttpStatusCode code)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock(responseContent, code);
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            //Assert
            Assert.ThrowsAsync<ApiException>(async () => await apiClient.GetAsync(url));
        }

        [Test]
        [TestCase("https://api", "", HttpStatusCode.InternalServerError)]
        public void GetAsync_UnknowStatusCode_CorrectExceptionMessage(string url, string responseContent, HttpStatusCode code)
        {
            //Arrange
            var messageHandler = new HttpMessageHandlerMock(responseContent, code);
            var httpClient = new HttpClient(messageHandler);
            var clientFactory = Substitute.For<IHttpClientFactory>();
            clientFactory.CreateClient().Returns(httpClient);
            var apiClient = new ApiClient(clientFactory, _apiConfig);

            //Act
            //Assert
            var ex = Assert.ThrowsAsync<ApiException>(async () => await apiClient.GetAsync(url));
            Assert.That(ex.Message, Is.EqualTo($"The HTTP status code of the response was not expected ({code})."));
        }
    }
}
