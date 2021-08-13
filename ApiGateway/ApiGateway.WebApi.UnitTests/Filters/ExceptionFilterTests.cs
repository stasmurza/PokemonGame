using ApiGateway.BusinessLayer.Exceptions;
using ApiGateway.WebApi.Filters;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;

namespace ApiGateway.WebApi.UnitTests.Filters
{
    [TestFixture]
    public class ExceptionFilterTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture()
            .Customize(new AutoNSubstituteCustomization()
            {
                ConfigureMembers = true,
            });
            _fixture.Customize<BindingInfo>(c => c.OmitAutoProperties());
        }

        [Test]
        public void OnException_LoggerShouldBeCalled()
        {
            var exception = new Exception("test");
            var logger = Substitute.For<ILogger<ExceptionFilter>>();
            var environment = Substitute.For<IWebHostEnvironment>();
            environment.EnvironmentName.Returns("Development");
            var executedContext = _fixture.Create<ExceptionContext>();
            executedContext.Exception = exception;

            var sut = Substitute.For<ExceptionFilter>(logger, environment);
            sut.OnException(executedContext);

            logger.Received().LogError(exception, exception.Message);
        }

        [Test]
        public void OnException_ApiBadRequestException_ContextResultShouldContainDetailsOfException()
        {
            var exception = new ApiBadRequestException("test");
            var logger = Substitute.For<ILogger<ExceptionFilter>>();
            var environment = Substitute.For<IWebHostEnvironment>();
            environment.EnvironmentName.Returns("Production");
            var executedContext = _fixture.Create<ExceptionContext>();
            executedContext.Exception = exception;

            var sut = Substitute.For<ExceptionFilter>(logger, environment);
            sut.OnException(executedContext);

            Assert.That(executedContext.Result, Is.TypeOf<BadRequestObjectResult>());
            var result = executedContext.Result as BadRequestObjectResult;
            Assert.That(result.Value, Is.EqualTo(exception.Message));
        }

        [Test]
        public void OnException_IsDevelopment_ContextResultShouldContainDetailsOfException()
        {
            var exception = new Exception("test");
            var logger = Substitute.For<ILogger<ExceptionFilter>>();
            var environment = Substitute.For<IWebHostEnvironment>();
            environment.EnvironmentName.Returns("Development");
            var executedContext = _fixture.Create<ExceptionContext>();
            executedContext.Exception = exception;

            var sut = Substitute.For<ExceptionFilter>(logger, environment);
            sut.OnException(executedContext);

            Assert.That(executedContext.Result, Is.TypeOf<BadRequestObjectResult>());
            var result = executedContext.Result as BadRequestObjectResult;
            Assert.That(result.Value, Is.EqualTo(exception.Message));
        }

        [Test]
        public void OnException_Exception_ContextResultShouldContainGeneralMessage()
        {
            var exception = new Exception("test");
            var logger = Substitute.For<ILogger<ExceptionFilter>>();
            var environment = Substitute.For<IWebHostEnvironment>();
            environment.EnvironmentName.Returns("Production");
            var executedContext = _fixture.Create<ExceptionContext>();
            executedContext.Exception = exception;

            var sut = Substitute.For<ExceptionFilter>(logger, environment);
            sut.OnException(executedContext);

            Assert.That(executedContext.Result, Is.TypeOf<BadRequestObjectResult>());
            var result = executedContext.Result as BadRequestObjectResult;
            Assert.That(result.Value, Is.EqualTo("Internal server error"));
        }
    }
}
