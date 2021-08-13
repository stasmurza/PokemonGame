using ApiGateway.WebApi.Filters;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;

namespace ApiGateway.WebApi.UnitTests.Filters
{
    [TestFixture]
    public class ActivityLoggerFilterTests
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
        public void OnActionExecuted_LogActionDescriptor()
        {
            var logger = Substitute.For<ILogger<ActivityLoggerFilter>>();
            var executedContext = _fixture.Create<ActionExecutedContext>();

            var sut = Substitute.For<ActivityLoggerFilter>(logger);
            sut.OnActionExecuted(executedContext);

            logger.ReceivedWithAnyArgs().LogInformation(String.Empty);
        }

        [Test]
        public void OnActionExecuting_LogActionDescriptor()
        {
            var logger = Substitute.For<ILogger<ActivityLoggerFilter>>();
            var executingContext = _fixture.Create<ActionExecutingContext>();

            var sut = Substitute.For<ActivityLoggerFilter>(logger);
            sut.OnActionExecuting(executingContext);

            logger.ReceivedWithAnyArgs().LogInformation(String.Empty);
        }
    }
}
