﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace HipsterTechnologies.API.Main
{
    public class Program
    {
        /// <summary>
        /// Entry point into the program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            // Pull the port to host the service on from our application-level
            // configuration. We can make this vary based on environment.
            var port = ConfigurationManager.AppSettings["port"];
            var url = String.Format("http://+:8080");

            // Run the web server until the user hits enter.
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Hipster Technologies (tm) (r) (#REKT) API Web Service");
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to stop");
                Console.ReadLine();
            }
        }
    }
}