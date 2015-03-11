using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using SimpleLogging.Core;
using System.Threading.Tasks;
using System.Threading;
using Nancy.Authentication.Token;
using Nancy.Security;
using HipsterTechnologies.API.Models;
using HipsterTechnologies.API.Models.Contexts;
using System.Reflection;
using System.Diagnostics;
using HipsterTechnologies.API.Routes.Context;
using Nancy.ModelBinding;

namespace HipsterTechnologies.API.Routes.Modules
{
    /// <summary>
    /// Module which is response for handling authentication for the API.
    /// Hit this to get a token.
    /// </summary>
    public class AuthModule : NancyModule
    {
        public AuthModule(ITokenizer tokenizer, IModelContextFactory dbFactory)
            : base("/api/auth")
        {
            // Store dependencies.
            _dbFactory = dbFactory;

            Get["/"] = _ =>
            {
                // Access token from the client. Technically this
                // comes from facebook.
                string facebookUserId = _.access_token;

                // Create or pull a user from the data.
                User user = null;
                using (var dbContext = _dbFactory.CreateModelContext())
                {
                    // Check to see if we actually have a user.
                    user = dbContext.Users.Find(facebookUserId);

                    // If we couldn't find a user with the given token as a user ID,
                    // create a new one.
                    if (user == default(User))
                    {
                        // Create a new user.
                        user = new User
                        {
                            UserId = facebookUserId,
                            UserName = facebookUserId
                        };

                        // Insert the user into the db.
                        var result = dbContext.Users.Add(user);

                        // Lock it up.
                        dbContext.SaveChanges();
                    }
                }

                // If we still don't have a user for some reason,
                // indicate failure.
                if (user == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                // Create an object containing the token as a property so that
                // the serialized output is within a json object.
                var token = tokenizer.Tokenize(user, Context);
                return new
                {
                    token = token,
                };
            };

            // TODO: Leaving this for Corban's testing purposes.
            Get["/validation"] = _ =>
            {
                this.RequiresAuthentication();
                return "Yay! You are authenticated!";
            };
        }

        private IModelContextFactory _dbFactory;
    }
}