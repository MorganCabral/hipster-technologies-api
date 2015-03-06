using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Threading.Tasks;
using SimpleLogging.Core;
using System.Threading;

namespace HipsterTechnologies.API.Routes.Modules
{
    /// <summary>
    /// Routes for Transaction-related endpoints.
    /// </summary>
    public class TransactionModule : NancyModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">A logger implementation.</param>
        public TransactionModule(ILoggingService logger) : base("/transactions")
        {
            // Set the logger.
            _logger = logger;

            // Set up endpoints.
            Get["/", true] = GetTransactions;
            Post["/", true] = PostTransactions;
            Delete["/", true] = PurgeTransactions;
        }

        /// <summary>
        /// Handles export and transaction querying duties.
        /// 
        /// For export, just return everything. This might take a while,
        /// hence why this route is async.
        /// 
        /// For transaction data querying, use the query string to specify
        /// what you want to filter by.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> GetTransactions(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Get /transactions");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        /// <summary>
        /// Handles import and transaction creation duties.
        /// 
        /// For import, just create a ton of transactions, and make sure
        /// that holdings are updated accordingly. This could take a while,
        /// hence why this route is async.
        /// 
        /// For buy/sell transaction creation duties, provide a single
        /// transaction object.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> PostTransactions(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Post /transactions");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        /// <summary>
        /// Purge the user's transaction history.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> PurgeTransactions(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Delete /transactions");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        private ILoggingService _logger;
    }
}