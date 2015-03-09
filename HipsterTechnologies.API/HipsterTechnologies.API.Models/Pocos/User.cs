using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;

namespace HipsterTechnologies.API.Models
{
    public class User
    {
        /// <summary>
        /// Primary Key. Facebook User ID.
        /// </summary>
        public string UserId { get; set; }

        public string UserName { get; set; }

        public List<string> Claims { get; set; }
    }
}