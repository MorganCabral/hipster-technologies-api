using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.TinyIoc;
using SimpleLogging.NLog;
using SimpleLogging.Core;

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
        }
    }
}
