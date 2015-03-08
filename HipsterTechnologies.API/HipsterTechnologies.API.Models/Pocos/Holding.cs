using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipsterTechnologies.API.Models
{
    /// <summary>
    /// Representation of a user's holdings for a particular stock.
    /// </summary>
    public class Holding
    {
        /// <summary>
        /// Primary Key.
        /// </summary>
        public int HoldingId { get; set; }

        /// <summary>
        /// The holding owner's facebook handle.
        /// </summary>
        public String FacebookHandle { get; set; }

        /// <summary>
        /// The exchange that the stock is traded on.
        /// </summary>
        public String Exchange { get; set; }

        /// <summary>
        /// The symbol that the stock is sold under.
        /// </summary>
        public String Symbol { get; set; }

        /// <summary>
        /// The quantity of the stock that is held.
        /// </summary>
        public int Quantity { get; set; }
    }
}