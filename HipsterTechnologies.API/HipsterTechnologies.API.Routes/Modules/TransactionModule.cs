using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using System.Threading.Tasks;
using SimpleLogging.Core;
using System.Threading;
using HipsterTechnologies.API.Models;
using HipsterTechnologies.API.Models.Contexts;
using HipsterTechnologies.API.Services.MarkIt;

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
        public TransactionModule(ILoggingService logger,
            IModelContextFactory dbFactory, 
            IStockMarketService stockMarketService) : base("/transactions")
        {
            // Hold on to dependencies.
            _logger = logger;
            _dbFactory = dbFactory;
            _stockMarketService = stockMarketService;

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
            return HttpStatusCode.NotImplemented;
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
            _logger.Info("POST /transactions");

            // Create a response object early on so that we can modify it
            // as we go.
            Response response = new Nancy.Response();

            // Bind the incoming request body to the transaction model.
            // We'll go ahead and blacklist the posted time and transaction id
            // since they haven't been created yet. Anything the user
            // provides is at best wrong and at worst malicious.
            var transaction = this.Bind<Transaction>( t => t.PostedTime, t => t.TransactionId );

            // Annotate the transaction with data.
            transaction.PostedTime = DateTime.Now;

            // Perform a price lookup on each stock
            foreach( var item in transaction.TransactionItems )
            {
                // Do a price lookup on the stock.
                var stock = await _stockMarketService.Quote(item.Symbol);
                item.PostedPrice = stock.LastPrice;
            }

            // Store the annotated transaction in the data. We also need to
            // update our internal record of the user's total stock holdings.
            using (var dbContext = _dbFactory.CreateModelContext())
            {
                // Store the transaction in the database.
                var result = dbContext.Transactions.Add(transaction);
                await dbContext.SaveChangesAsync();

                // Set the location header now that we have a final
                // transaction ID to work with.
                String location = String.Format("/transactions/{0}", result.TransactionId);
                response.Headers.Add("Location", location);
            }

            // Making it this far implies that we have succeeded.
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        /// <summary>
        /// Purge the user's transaction history.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> PurgeTransactions(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        private ILoggingService _logger;
        private IModelContextFactory _dbFactory;
        private IStockMarketService _stockMarketService;
    }
}