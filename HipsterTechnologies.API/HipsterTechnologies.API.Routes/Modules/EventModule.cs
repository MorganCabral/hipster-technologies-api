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
        public EventModule(ILoggingService logger) : base("/events")
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
            _logger.Info("Get /events/{0}", parameters.id);
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        public async Task<dynamic> CreateEvent(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Post /events", parameters.id);
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        public async Task<dynamic> UpdateEvent(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Put /events/{0}", parameters.id);
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        public async Task<dynamic> DeleteEvent(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Delete /events/{0}", parameters.id);
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }


        public async Task<dynamic> ImportEvents(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Post /events/import");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        public async Task<dynamic> ExportEvents(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Get /events/export");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        private ILoggingService _logger;
    }
}