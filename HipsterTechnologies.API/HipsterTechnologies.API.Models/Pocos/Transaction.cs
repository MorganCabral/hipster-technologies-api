using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HipsterTechnologies.API.Models
{
    /// <summary>
    /// Used to distinguish between stock sales and purchases.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// The transaction represents the sale of stock.
        /// </summary>
        Sale,

        /// <summary>
        /// The transaction represents the purchase of stock.
        /// </summary>
        Purchase
    }

    public class TransactionItem
    {
        /// <summary>
        /// Primary Key.
        /// </summary>
        public int TransactionItemId { get; set; }

        /// <summary>
        /// The exchange where the stock is traded.
        /// </summary>
        public String Exchange { get; set; }

        /// <summary>
        /// The stock symbol to trade.
        /// </summary>
        public String Symbol { get; set; }

        /// <summary>
        /// The type of the transaction (purchase/sale).
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// The quantity of stock to buy/sell.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The price that the stock was purchased/sold for at the time
        /// of posting.
        /// </summary>
        public Decimal PostedPrice { get; set; }

        /// <summary>
        /// The date and time when the transaction item was made.
        /// </summary>
        public DateTime PostedDateTime { get; set; }
    }

    /// <summary>
    /// Representation of a stock transaction made by the user.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Primary Key.
        /// </summary>
        public int TransactionId { get; set; }

        /// <summary>
        /// The user's facebook handle.
        /// </summary>
        public String FacebookHandle { get; set; }

        /// <summary>
        /// The date and time when the transaction was posted.
        /// </summary>
        public DateTime PostedDateTime { get; set; }

        /// <summary>
        /// A list containing the data about each stock that is part of
        /// the transaction.
        /// </summary>
        public virtual List<TransactionItem> TransactionItems { get; set; }

    }
}