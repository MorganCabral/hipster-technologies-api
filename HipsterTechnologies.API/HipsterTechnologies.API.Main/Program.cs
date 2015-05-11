using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace HipsterTechnologies.API.Main
{
    public class ChatSocket : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(string.Format("Message: {0}", e.Data));

            Sessions.Broadcast(e.Data);
        }

        protected override void OnOpen()
        {
            Console.WriteLine("Chat connection opened.");
        }
    }

    public class Program
    {
        /// <summary>
        /// Entry point into the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            // Log something so that the user knows we're actually doing something.
            Console.WriteLine("Hipster Technologies (tm) (r) (#REKT) API Web Service");

            // Pull the port to host the service on from our application-level
            // configuration. We can make this vary based on environment.
            var port = ConfigurationManager.AppSettings["port"];
            //var url = String.Format("http://+:80/");
            var url = String.Format("http://+:8080/");

            //var wssv = new WebSocketServer("ws://rekt.corb.co:8081");
            var wssv = new WebSocketServer("ws://localhost:8081");
            wssv.AddWebSocketService<ChatSocket>("/chat");
            wssv.Start();

            // Run the web server until the user hits enter.
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0} - press enter to stop...", url);
                Console.ReadLine();
                wssv.Stop();
            }
        }
    }
}
