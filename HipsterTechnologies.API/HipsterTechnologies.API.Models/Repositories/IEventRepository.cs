using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Models.Repositories
{
    /// <summary>
    /// Interface for Repository objects that interact with Holding objects.
    /// </summary>
    public interface IEventRepository
    {
        /// <summary>
        /// Inserts the instance of the event object into the DB.
        /// </summary>
        /// <param name="theEvent">The event object.</param>
        /// <returns>The event object, with annotations from Entity Framework.</returns>
        Event Create(Event theEvent);

        /// <summary>
        /// Retrieve events from the given user using the specified filter queries.
        /// </summary>
        /// <param name="username">The username of the user who is associated with the events.</param>
        /// <param name="queries">Queries which will be used to filter the events.</param>
        /// <returns>A list of events that the specified queries.</returns>
        IEnumerable<Event> GetEvents(String username, params Func<Event, Boolean>[] filters);

        /// <summary>
        /// Retrieve all events from the given user.
        /// </summary>
        /// <param name="username">The username of the user who is associated with the events.</param>
        /// <returns>A list of events.</returns>
        IEnumerable<Event> GetEvents(String username);

        /// <summary>
        /// Updates the specified event in the database.
        /// </summary>
        /// <param name="theEvent">The event to update.</param>
        void Update(Event theEvent);

        /// <summary>
        /// Delete the event in the database.
        /// </summary>
        /// <param name= "theEvent">The event to delete.</param>
        void Delete(Event theEvent);
    }
}
