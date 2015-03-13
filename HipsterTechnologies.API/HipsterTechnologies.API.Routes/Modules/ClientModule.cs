using Nancy;
using SimpleLogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HipsterTechnologies.API.Routes.Modules
{
    public class ClientModule : NancyModule
    {
        public ClientModule(ILoggingService logger) : base("/")
        {
            Get["/", true] = GetIndex;
            Get["(.*)", true] = GetIndex;
        }

        public async Task<dynamic> GetIndex(dynamic parameters, CancellationToken token)
        {
            return View["Client/dist/index.html"];
        }
    }
}