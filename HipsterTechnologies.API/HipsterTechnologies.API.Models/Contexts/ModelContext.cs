using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace HipsterTechnologies.API.Models.Contexts
{
    /// <summary>
    /// Context class which is used to access model objects.
    /// </summary>
    public class ModelContext : DbContext
    {
        /// <summary>
        /// Accessor property for Holding models.
        /// </summary>
        public virtual IDbSet<Holding> Holdings { get; set; }

        /// <summary>
        /// Accessor property for Transaction models.
        /// </summary>
        public virtual IDbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Accessor property for TransactionItem models.
        /// </summary>
        public virtual IDbSet<TransactionItem> TransactionItems { get; set; }

        /// <summary>
        /// Accessor property for User models.
        /// </summary>
        public virtual IDbSet<User> Users { get; set; }

        /// <summary>
        /// Mark the given entity as modified.
        /// </summary>
        /// <param name="entity">The entity to mark as modified.</param>
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
