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
        public int Id { get; set; }

        /// <summary>
        /// The event owner's facebook handle.
        /// </summary>
        public String FacebookId { get; set; }

        /// <summary>
        /// The start time of the event.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The amount of time the event lasts for.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Whether or not the event lasts all day.
        /// </summary>
        public Boolean AllDay { get; set; }

        /// <summary>
        /// The name of the event.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The description of the event.
        /// </summary>
        public String Description { get; set; }

    }
}