using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace HipsterTechnologies.API.Models
{
    /// <summary>
    /// Context class which is used to access model objects.
    /// </summary>
    public class HipsterDbContext : DbContext
    {
        /// <summary>
        /// Accessor property for holding models.
        /// </summary>
        public DbSet<Holding> Holdings { get; set; }

        /// <summary>
        /// Accessor property for transaction models.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }
    }
}
