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
    /// Routes for Facebook-related endpoints.
    /// </summary>
    public class FacebookModule : NancyModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">A logger implementation.</param>
        public FacebookModule(ILoggingService logger) : base("/facebook")
        {
            // Store the logger.
            _logger = logger;

            // Set up route handlers for friend management.
            Get["/friends", true] = GetFriends;
            Post["/friends", true] = AddFriend;

            // ...etc.
        }

        public async Task<dynamic> GetFriends(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Get /facebook/friends");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        public async Task<dynamic> AddFriend(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Post /facebook/friends");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        private ILoggingService _logger;
    }
}