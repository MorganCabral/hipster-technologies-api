using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;

namespace HipsterTechnologies.API.Main
{
    /// <summary>
    /// OWIN startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Instructs OWIN to use our NancyFX app.
        /// </summary>
        /// <param name="app">The OWIN app.</param>
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
