﻿// <copyright file="StockEntity.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Repository
{
    using System.Collections.Generic;

    /// <summary>
    /// Stock class.
    /// </summary>
    public class StockEntity
    {
        /// <summary>
        /// Gets or sets the stock ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  Gets or sets the stock symbol.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Gets or sets the stock name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the listing stock exchange or market of the security.
        /// </summary>
        public string Exchange { get; set; }

        /// <summary>
        /// Gets or sets list of stock prices.
        /// </summary>
        public List<StockPriceEntity> StockPrices { get; set; }
    }
}