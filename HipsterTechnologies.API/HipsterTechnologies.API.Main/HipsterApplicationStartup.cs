using Nancy.Bootstrapper;
using SimpleLogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Main
{
    public class HipsterApplicationStartup : IApplicationStartup
    {
        public HipsterApplicationStartup(ILoggingService logger)
        {
            _logger = logger;
        }

        public void Initialize(IPipelines pipelines)
        {
            // Trace the logger initialization at the least visible level.
            // NLog takes forever to load in symbols, so we might as well
            // do it at application startup to prevent confusion on
            // the user's part.
            _logger.Trace("Initialized the logger");

            // Set up the pre-request pipeline.
            pipelines.BeforeRequest += (ctx) =>
            {
                _logger.Info("{0} {1}", ctx.Request.Method.ToUpper(), ctx.Request.Path);
                _logger.Debug(ctx.Request.Body.ToString());
                return null;
            };

            // Set up the post-request pipeline.

            // Set up the error-handler pipeline.
        }

        private ILoggingService _logger;
    }
}
