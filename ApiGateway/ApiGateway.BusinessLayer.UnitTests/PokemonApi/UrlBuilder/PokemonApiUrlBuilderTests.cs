using ApiGateway.BusinessLayer.PokemonApi.UrlBuilder;
using NUnit.Framework;
using System;
using System.Web;

namespace ApiGateway.BusinessLayer.UnitTests.PokemonApi.UrlBuilder
{
    [TestFixture]
    public class PokemonApiUrlBuilderTests
    {
        [Test]
        public void ByNameUrl_CorrectParameters_CorrectUrl()
        {
            //Arrange
            var apiUrl = "https://api";
            var name = "test";
            var urlBuilder = new PokemonApiUrlBuilder();

            //Act
            var url = urlBuilder.ByNameUrl(apiUrl, name);

            //Assert
            Assert.That(url, Is.EqualTo($"{apiUrl}/byname/{HttpUtility.UrlEncode(name)}"));
        }

        [Test]
        public void ByNameUrl_CorrectParameters_UrlEncodeShouldBeApplied()
        {
            //Arrange
            var apiUrl = "https://api";
            var name = "test test";
            var urlBuilder = new PokemonApiUrlBuilder();

            //Act
            var url = urlBuilder.ByNameUrl(apiUrl, name);

            //Assert
            Assert.That(url, Is.EqualTo($"{apiUrl}/byname/{HttpUtility.UrlEncode(name)}"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ByNameUrl_EmptyName_ThrowException(string name)
        {
            //Arrange
            var apiUrl = "https://api";
            var urlBuilder = new PokemonApiUrlBuilder();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => urlBuilder.ByNameUrl(apiUrl, name));
        }

        [TestCase("")]
        [TestCase(null)]
        public void ByNameUrl_EmptyApiUrl_ThrowException(string apiUrl)
        {
            //Arrange
            var name = "test";
            var urlBuilder = new PokemonApiUrlBuilder();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => urlBuilder.ByNameUrl(apiUrl, name));
        }
    }
}
