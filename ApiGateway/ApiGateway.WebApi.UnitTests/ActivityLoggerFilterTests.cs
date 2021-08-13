using ApiGateway.WebApi.Filters;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace ApiGateway.WebApi.UnitTests
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

            var sut = _fixture.Create<ActivityLoggerFilter>();
            sut.OnActionExecuted(executedContext);

            logger.ReceivedWithAnyArgs(2);
        }

        [Test]
        public void OnActionExecuting_LogActionDescriptor()
        {
            var logger = Substitute.For<ILogger<ActivityLoggerFilter>>();
            var executingContext = _fixture.Create<ActionExecutingContext>();

            var sut = _fixture.Create<ActivityLoggerFilter>();
            sut.OnActionExecuting(executingContext);

            logger.ReceivedWithAnyArgs(2);
        }
    }
}
