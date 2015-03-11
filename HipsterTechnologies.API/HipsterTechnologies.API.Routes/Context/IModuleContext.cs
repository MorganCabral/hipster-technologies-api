using HipsterTechnologies.API.Models.Contexts;
using SimpleLogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Routes.Context
{
    /// <summary>
    /// Module-level context object.
    /// </summary>
    public interface IModuleContext
    {
        /// <summary>
        /// Access to a logger object.
        /// </summary>
        ILoggingService Logger { get; set; }

        /// <summary>
        /// Access to the ModelContext factory object.
        /// </summary>
        IModelContextFactory DbFactory { get; set; }

        /// <summary>
        /// Get the API Version. Use this for versioning in routes.
        /// </summary>
        String ApiVersion { get; }
    }
}
