using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipsterTechnologies.API.Models
{
    /// <summary>
    /// Representation of a stock transaction made by the user.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Primary Key.
        /// </summary>
        public int TransactionId { get; set; }
    }
}