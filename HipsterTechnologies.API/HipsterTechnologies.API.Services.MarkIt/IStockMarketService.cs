using HipsterTechnologies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Services.MarkIt
{
    /// <summary>
    /// Service for interface with stock market information providers.
    /// </summary>
    public interface IStockMarketService
    {
        /// <summary>
        /// Attempt to look up stocks based on a search term.
        /// </summary>
        /// <param name="searchTerm">A searh term.</param>
        /// <returns>A list potentially containing stock information.</returns>
        Task<IEnumerable<Stock>> Lookup(String searchTerm);

        /// <summary>
        /// Retrieve a stock quote.
        /// </summary>
        /// <param name="exchange">The exchange to look for the symbol on.</param>
        /// <param name="symbol">The symbol of the stock we want to get a quote of.</param>
        /// <returns>A decimal containing the current price of the stock.</returns>
        Task<Stock> Quote(String symbol);
    }
}
