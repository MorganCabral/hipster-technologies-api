using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Nancy;
using SimpleLogging.Core;
using Moq;
using Nancy.Testing;
using Nancy.Bootstrapper;
using HipsterTechnologies.API.Routes;

namespace HipsterTechnologies.API.Routes.Tests
{
    /// <summary>
    /// Unit Tests for our custom bootstrapper.
    /// </summary>
    [TestFixture]
    public class HipsterApplicationStartupTests
    {
        #region Mock Objects

        public Mock<ILoggingService> LoggerMock { get; set; }

        public Browser Browser { get; set; }

        #endregion

        #region Test Module

        class TestModule : NancyModule
        {
            public TestModule() : base("tests")
            {
                Get["/"] = _ => { return HttpStatusCode.NotImplemented; };
                Post["/"] = _ => { return HttpStatusCode.NotImplemented; };
                Delete["/"] = _ => { return HttpStatusCode.NotImplemented; };
                Put["/"] = _ => { return HttpStatusCode.NotImplemented; };
            }
        }

        #endregion

        [SetUp]
        public void SetUp()
        {
            LoggerMock = new Mock<ILoggingService>();
            var appStartup = new HipsterApplicationStartup(LoggerMock.Object);

            Browser = new Browser(cfg => {
                cfg.Module<TestModule>();
                cfg.Dependency<ILoggingService>(LoggerMock.Object);
                cfg.Dependency<IApplicationStartup>(appStartup);
            });
        }

        [Test]
        public void TestLogsHttpMethodAndEndpoint()
        {
            Browser.Get("/tests");
            Browser.Post("/tests");
            Browser.Delete("/tests");
            Browser.Put("/tests");

            LoggerMock.Verify(mock => mock.Info("{0} {1}", "GET", "/tests"), Times.Once);
            LoggerMock.Verify(mock => mock.Info("{0} {1}", "POST", "/tests"), Times.Once);
            LoggerMock.Verify(mock => mock.Info("{0} {1}", "DELETE", "/tests"), Times.Once);
            LoggerMock.Verify(mock => mock.Info("{0} {1}", "PUT", "/tests"), Times.Once);
        }
    }
}
