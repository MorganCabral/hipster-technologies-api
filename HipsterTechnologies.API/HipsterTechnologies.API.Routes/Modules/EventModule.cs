using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using System.Threading.Tasks;
using SimpleLogging.Core;
using System.Threading;
using HipsterTechnologies.API.Models;
using HipsterTechnologies.API.Models.Contexts;
using HipsterTechnologies.API.Services.MarkIt;
using System.Data.Entity;
using Nancy.Responses;
using Nancy.Security;
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
        public EventModule(ILoggingService logger, 
            IModelContextFactory dbFactory ) : base("/api/events")
        {
            _logger = logger;
            _dbfactory = dbFactory;

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
            var results = new List<Event>();
            var username = this.Context.CurrentUser.UserName;
            using (var dbContext = _dbfactory.CreateModelContext())
            {
                results = dbContext.Events
                    .Where(tx => tx.FacebookId == username)
                    .ToList(); 
            }


            return results;
        }

        public async Task<dynamic> CreateEvent(dynamic parameters, CancellationToken token)
        {
            Response response = new Nancy.Response();
            var newEvent = this.Bind<Event>(t => t.Id);
            // Store the annotated transaction in the data. We also need to
            // update our internal record of the user's total stock holdings.
            using (var dbContext = _dbfactory.CreateModelContext())
            {
                // Store the transaction in the database.
                var result = dbContext.Events.Add(newEvent);
                await dbContext.SaveChangesAsync();

                // Set the location header now that we have a final
                // transaction ID to work with.
                String location = String.Format("{0}/{1}", ModulePath, result.Id);
                response.Headers.Add("Location", location);
            }

            // Making it this far implies that we have succeeded.
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        public async Task<dynamic> UpdateEvent(dynamic parameters, CancellationToken token)
        {
            Response response = new Nancy.Response();
            var newEvent = this.Bind<Event>();
            // Store the annotated transaction in the data. We also need to
            // update our internal record of the user's total stock holdings.
            using (var dbContext = _dbfactory.CreateModelContext())
            {
                // Store the transaction in the database.
                var result = dbContext.Events.Update(newEvent);
                await dbContext.SaveChangesAsync();

                // Set the location header now that we have a final
                // transaction ID to work with.
                String location = String.Format("{0}/{1}", ModulePath, result.Id);
                response.Headers.Add("Location", location);
            }

            // Making it this far implies that we have succeeded.
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        public async Task<dynamic> DeleteEvent(dynamic parameters, CancellationToken token)
        {
            Response response = new Nancy.Response();
            var newEvent = this.Bind<Event>();
            // Store the annotated transaction in the data. We also need to
            // update our internal record of the user's total stock holdings.
            using (var dbContext = _dbfactory.CreateModelContext())
            {
                // Store the transaction in the database.
                var result = dbContext.Events.Remove(newEvent);
                await dbContext.SaveChangesAsync();

                // Set the location header now that we have a final
                // transaction ID to work with.
                String location = String.Format("{0}/{1}", ModulePath, result.Id);
                response.Headers.Add("Location", location);
            }

            // Making it this far implies that we have succeeded.
            response.StatusCode = HttpStatusCode.OK;
            return response;
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
        private IModelContextFactory _dbfactory;
    }
}