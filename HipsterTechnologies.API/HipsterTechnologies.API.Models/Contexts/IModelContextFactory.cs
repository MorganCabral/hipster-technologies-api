using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Models.Contexts
{
    /// <summary>
    /// Factory Method pattern implementation which creates ModelContexts.
    /// This is primarily useful for mocking out the database access code
    /// in places where it is needed (ie. in the Routes project).
    /// </summary>
    public interface IModelContextFactory
    {
        /// <summary>
        /// Creates and returns an instance of the ModelContext class.
        /// </summary>
        /// <returns>An instance of the ModelContext class.</returns>
        ModelContext CreateModelContext();
    }
}
