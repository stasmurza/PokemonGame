using ApiGateway.BusinessLayer.TranslateApi.UrlBuilder;
using NUnit.Framework;
using System;
using System.Web;

namespace ApiGateway.UnitTests.TranslateApi.UrlBuilder
{
    public class TranslateApiUrlBuilderTests
    {
        [Test]
        public void Build_CorrectParameters_CorrectUrl()
        {
            //Arrange
            var apiUrl = "https://api/";
            var text = "test";
            var urlBuilder = new TranslateApiUrlBuilder();

            //Act
            var url = urlBuilder.Build(apiUrl, text);

            //Assert
            Assert.That(url, Is.EqualTo($"{apiUrl}{HttpUtility.UrlEncode(text)}"));
        }

        [Test]
        public void Build_CorrectParameters_UrlEncodeShouldBeApplied()
        {
            //Arrange
            var apiUrl = "https://api/";
            var text = "test test";
            var urlBuilder = new TranslateApiUrlBuilder();

            //Act
            var url = urlBuilder.Build(apiUrl, text);

            //Assert
            Assert.That(url, Is.EqualTo($"{apiUrl}{HttpUtility.UrlEncode(text)}"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void Build_EmptyName_ThrowException(string text)
        {
            //Arrange
            var apiUrl = "https://api/";
            var urlBuilder = new TranslateApiUrlBuilder();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => urlBuilder.Build(apiUrl, text));
        }

        [TestCase("")]
        [TestCase(null)]
        public void Build_EmptyApiUrl_ThrowException(string apiUrl)
        {
            //Arrange
            var text = "test";
            var urlBuilder = new TranslateApiUrlBuilder();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => urlBuilder.Build(apiUrl, text));
        }
    }
}
