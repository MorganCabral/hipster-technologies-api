using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using SimpleLogging.Core;
using System.Threading.Tasks;
using System.Threading;

namespace HipsterTechnologies.API.Routes.Modules
{
    /// <summary>
    /// Routes for Event-related endpoints.
    /// </summary>
    public class EventModule : NancyModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">A logger implementation.</param>
        public EventModule(ILoggingService logger) : base("/api/events")
        {
            _logger = logger;

            // Event CRUD.
            Post["/", true] = CreateEvent;
            Get["/{id}", true] = GetEvent;
            Put["/{id}", true] = UpdateEvent;
            Delete["/{id}", true] = DeleteEvent;

            // Import endpoints.
            Get["/", true] = ImportEvents;
            Put["/", true] = ExportEvents;
        }

        public async Task<dynamic> GetEvent(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        public async Task<dynamic> CreateEvent(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        public async Task<dynamic> UpdateEvent(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        public async Task<dynamic> DeleteEvent(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }


        public async Task<dynamic> ImportEvents(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        public async Task<dynamic> ExportEvents(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        private ILoggingService _logger;
    }
}