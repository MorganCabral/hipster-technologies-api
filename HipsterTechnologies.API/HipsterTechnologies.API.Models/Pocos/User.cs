using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace HipsterTechnologies.API.Models
{

    /// <summary>
    /// Model class which represents the user.
    /// </summary>
    public class User : IUserIdentity
    {
        /// <summary>
        /// Primary Key. Token that facebook generates for our user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The user's facebook handle.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// We don't actually care about claims since we don't make use of them.
        /// Hence why we have effectively one level of access.
        /// </summary>
        [NotMapped]
        public IEnumerable<string> Claims
        {
            get
            {
                var claim = new[] { "User" };
                return new List<String>(claim);
            }
        }
    }
}