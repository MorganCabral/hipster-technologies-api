using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using System.Threading.Tasks;
using SimpleLogging.Core;
using System.Threading;
using HipsterTechnologies.API.Services.MarkIt;

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
        public StockModule(ILoggingService logger, IStockMarketService stockMarketService) : base("/stocks")
        {
            // Hold on to our logger implementation.
            _logger = logger;
            _stockMarketService = stockMarketService;

            // Setup route handlers.
            Get["/{exchange}/{symbol}", true] = GetStockInfo;
            Get["/report", true] = GetStockReport;
        }

        /// <summary>
        /// Retrieves a list of the user's top five stocks.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> GetStockReport(dynamic parameters, CancellationToken token)
        {
            return HttpStatusCode.NotImplemented;
        }

        /// <summary>
        /// Get information about a specific stock.
        /// </summary>
        /// <param name="parameters">Arguments from the client.</param>
        /// <param name="token">Token used to indicate that the task should stop.</param>
        /// <returns>A task containing the result of whatever we do in this handler.</returns>
        public async Task<dynamic> GetStockInfo(dynamic parameters, CancellationToken token)
        {
            var stock = await _stockMarketService.Quote(parameters.symbol);
            return stock;
        }

        private ILoggingService _logger;
        private IStockMarketService _stockMarketService;
    }
}