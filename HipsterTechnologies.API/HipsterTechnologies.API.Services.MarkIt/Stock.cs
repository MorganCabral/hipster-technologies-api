using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HipsterTechnologies.API.Services.MarkIt
{
    /// <summary>
    /// Representation of a stock.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// The exchange that the stock is traded on.
        /// </summary>
        public String Exchange { get; set; }

        /// <summary>
        /// The symbol that the company trades under on the given exchange.
        /// </summary>
        public String Symbol { get; set; }

        /// <summary>
        /// The name of the company that this stock represents.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The most recent price that the stock was bought/sold at.
        /// </summary>
        public Decimal LastPrice { get; set; }

        /// <summary>
        /// The last time the company's stock was traded.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The high price of the stock during the trading season.
        /// </summary>
        public Decimal High { get; set; }

        /// <summary>
        /// The low price of the stock during the trading season.
        /// </summary>
        public Decimal Low { get; set; }

        /// <summary>
        /// The opening price of the stock during the trading season.
        /// </summary>
        public Decimal Open { get; set; }
    }
}
