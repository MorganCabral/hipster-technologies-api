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

namespace HipsterTechnologies.API.Routes.Modules
{

    public class AuthModule : NancyModule
    {
        public AuthModule(ITokenizer tokenizer, IModelContextFactory dbFactory)
            : base("/auth")
        {
            _dbFactory = dbFactory;

            Get["/{oauthToken}"] = x =>
            {

                // TODO: Lookup the UID with the oath token
                string facebookUserId = "10153120273240159";

                IUserIdentity userIdentity = null;
                using (var dbContext = _dbFactory.CreateModelContext())
                {
                    userIdentity = dbContext.Users.FirstOrDefault(y => y.UserId == facebookUserId);
                }

                if (userIdentity == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var token = tokenizer.Tokenize(userIdentity, Context);

                return String.Format("Token: {0}\n UID: {1}", token, facebookUserId);

                return new
                {
                    Token = token,
                };
            };

            Get["/validation"] = _ =>
            {
                this.RequiresAuthentication();
                return "Yay! You are authenticated!";
            };

            Get["/admin"] = _ =>
            {
                this.RequiresClaims(new[] { "admin" });
                return "Yay! You are authorized!";
            };
        }

        private IModelContextFactory _dbFactory;
    }
}