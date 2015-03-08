using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Models.Contexts
{
    /// <summary>
    /// Factory Method implementation which creates ModelContext instances.
    /// </summary>
    public class ModelContextFactory : IModelContextFactory
    {
        /// <summary>
        /// Creates an instance of the ModelContext class.
        /// </summary>
        /// <returns>An instance of the ModelContext class.</returns>
        public ModelContext CreateModelContext()
        {
            return new ModelContext();
        }
    }
}
