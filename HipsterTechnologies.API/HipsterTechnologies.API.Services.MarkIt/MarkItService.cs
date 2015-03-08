using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HipsterTechnologies.API.Models;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HipsterTechnologies.API.Services.MarkIt
{
    /// <summary>
    /// C# Wrapper around the MarkIt web service for accessing
    /// stock market quotes and other data.
    /// </summary>
    public class MarkItService : IStockMarketService
    {
        /// <summary>
        /// Search for stocks based on the given search term.
        /// </summary>
        /// <param name="searchTerm">A search term.</param>
        /// <returns>A list potentially containing the stock.</returns>
        public async Task<IEnumerable<Stock>> Lookup(string searchTerm)
        {
            // Set up our HTTP client.
            var client = new RestClient("http://dev.markitondemand.com/Api/v2");

            // Build out the request.
            var request = new RestRequest("/Lookup/json", Method.GET);
            request.AddParameter("input", searchTerm);

            // Make the request and convert the resulting response into
            // something we can work with.
            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<List<Stock>>(response.Content);

            // Return the final list of stocks.
            return content;
        }

        /// <summary>
        /// Retrieve a stock quote.
        /// </summary>
        /// <param name="exchange">The exchange to look for the stock on.</param>
        /// <param name="symbol">The symbol of the stock.</param>
        /// <returns></returns>
        public async Task<Stock> Quote(string symbol)
        {
            // Set up our HTTP client.
            var client = new RestClient("http://dev.markitondemand.com/Api/v2");

            // Build out the request.
            var request = new RestRequest("/Quote/json", Method.GET);
            request.AddParameter("symbol", symbol);

            // Make the request and convert the resulting response into
            // something we can work with.
            var response = client.Execute(request);
            var content = JsonConvert.DeserializeObject<Stock>(response.Content,
                new IsoDateTimeConverter { DateTimeFormat = "ddd MMM d HH:mm:ss UTCzzzzz yyyy" });

            // MarkIt doesn't include the exchange with the stock quote data
            // for some reason. Let's fix that.
            var exchange = (await Lookup(content.Name))
                .Select(stock => stock.Exchange)
                .FirstOrDefault();
            content.Exchange = exchange;

            // Return the finalized stock.
            return content;
        }
    }
}
