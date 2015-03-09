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
                // DO NOT DO what this next line does. Shame on you.
                string facebookUserId = x.oauthToken;

                IUserIdentity userIdentity = null;
                using (var dbContext = _dbFactory.CreateModelContext())
                {
                    userIdentity = dbContext.Users.FirstOrDefault(y => y.UserId == facebookUserId);

                    if (userIdentity == null)
                    {
                        User newUser = new User();
                        newUser.UserId = facebookUserId;
                        // Don't do this.
                        newUser.UserName = facebookUserId;
                        List<string> claimsList = new List<string>();
                        claimsList.Add("nonadmin");
                        newUser.Claims = claimsList;

                        var result = dbContext.Users.Add(newUser);
                        dbContext.SaveChanges();

                        userIdentity = newUser;
                    }
                }

                //userIdentity.Claims = new List<string>();

                if (userIdentity == null || userIdentity.Claims == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var token = tokenizer.Tokenize(userIdentity, Context);

                return token;

                //return String.Format("Token: {0}\n UID: {1}", token, facebookUserId);

                //return new
                //{
                //    Token = token,
                //};
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