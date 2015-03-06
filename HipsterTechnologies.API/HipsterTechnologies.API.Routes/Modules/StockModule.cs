using Nancy;
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
    /// Routes for Stock-related endpoints.
    /// </summary>
    public class StockModule : NancyModule
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">A logger implementation.</param>
        public StockModule(ILoggingService logger) : base("/stocks")
        {
            // Hold on to our logger implementation.
            _logger = logger;

            // Setup route handlers.
            Get["/", true] = GetTopFiveStocks;
            Get["/{exchange}/{symbol}", true] = GetStockInfo;
        }

        /// <summary>
        /// Retrieves a list of the user's top five stocks.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> GetTopFiveStocks(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Get /stocks");
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        /// <summary>
        /// Get information about a specific stock.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> GetStockInfo(dynamic parameters, CancellationToken token)
        {
            _logger.Info("Get /stocks/{0}/{1}", parameters.exchange, parameters.symbol);
            await Task.Delay(1000);
            return "Arble Garble Warble";
        }

        private ILoggingService _logger;
    }
}