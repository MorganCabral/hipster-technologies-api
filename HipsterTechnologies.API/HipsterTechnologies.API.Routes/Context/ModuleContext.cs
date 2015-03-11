using HipsterTechnologies.API.Models.Contexts;
using SimpleLogging.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HipsterTechnologies.API.Routes.Context
{
    public class ModuleContext : IModuleContext
    {
        /// <summary>
        /// Access to a logger object.
        /// </summary>
        public ILoggingService Logger { get; set; }

        /// <summary>
        /// Access to the ModelContext factory object.
        /// </summary>
        public IModelContextFactory DbFactory { get; set; }
        
        /// <summary>
        /// Get the API Version. Use this for versioning in routes.
        /// </summary>
        public String ApiVersion
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                return fvi.FileMajorPart.ToString();
            }
        }
    }
}