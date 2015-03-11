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
    public interface IHoldingRepository
    {
        /// <summary>
        /// Inserts the instance of the holding object into the DB.
        /// </summary>
        /// <param name="holding">The holding object.</param>
        /// <returns>The holding object, with annotations from Entity Framework.</returns>
        Holding Create(Holding holding);

        /// <summary>
        /// Retrieve holdings from the given user using the specified filter queries.
        /// </summary>
        /// <param name="username">The username of the user who is associated with the holdings.</param>
        /// <param name="queries">Queries which will be used to filter the holdings.</param>
        /// <returns>A list of holdings that the specified queries.</returns>
        IEnumerable<Holding> GetHoldings(String username, params Func<Holding, Boolean>[] filters);

        /// <summary>
        /// Retrieve all holdings from the given user.
        /// </summary>
        /// <param name="username">The username of the user who is associated with the holdings.</param>
        /// <returns>A list of holdings.</returns>
        IEnumerable<Holding> GetHoldings(String username);

        /// <summary>
        /// Updates the specified holding in the database.
        /// </summary>
        /// <param name="holding">The holding to update.</param>
        void Update(Holding holding);

        /// <summary>
        /// Delete the holding in the database.
        /// </summary>
        /// <param name="holding">The holding to delete.</param>
        void Delete(Holding holding);
    }
}
