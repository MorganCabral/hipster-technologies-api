using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipsterTechnologies.API.Models
{
    /// <summary>
    /// Representation of a Calendar event given to a user.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Primary Key.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// The event owner's facebook handle.
        /// </summary>
        public String FacebookHandle { get; set; }

        /// <summary>
        /// The start time of the event.
        /// </summary>
        public DateTime EventStart { get; set; }

        /// <summary>
        /// The amount of time the event lasts for.
        /// </summary>
        public DateTimeOffset EventLength { get; set; }

        /// <summary>
        /// The description of the event.
        /// </summary>
        public String EventName { get; set; }
    }
}