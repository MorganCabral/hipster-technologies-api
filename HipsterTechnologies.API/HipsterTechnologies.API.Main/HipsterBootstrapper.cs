using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.TinyIoc;
using Nancy.Authentication.Token;
using SimpleLogging.NLog;
using SimpleLogging.Core;
using Nancy.Bootstrapper;
using HipsterTechnologies.API.Models.Contexts;
using HipsterTechnologies.API.Services.MarkIt;

namespace HipsterTechnologies.API.Main
{
    public class HipsterBootstrapper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// Compose service classes and other project-wide things here.
        /// </summary>
        /// <param name="container">The NancyFX IoC container.</param>
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            // Create our logging service and register it with
            // the IoC container. We want our controllers to be able to
            // access the service so we can actually log things.
            var loggingService = new NLogLoggingService();
            container.Register<ILoggingService>(loggingService);

            // Create and register an instance of the ModelContextFactory.
            // This abstracts away the process of creating ModelContexts,
            // making it easier to test (we can insert the factory
            // as a dependency and rely upon it to create contexts).
            // In our tests we simply mock this out and pass it as a 
            // dependency.
            var modelContextFactory = new ModelContextFactory();
            container.Register<IModelContextFactory>(modelContextFactory);

            // Create and register an instance of our stock market interfacing
            // service so that we can get stock quotes and other info.
            var stockMarketService = new MarkItService();
            container.Register<IStockMarketService>(stockMarketService);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
        }
    }
}
