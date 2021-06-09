// <copyright file="SymbolListResult.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.DataSource.Yahoo
{
    using System;
    using System.Collections.Generic;
    using Hilres.Stock.Updater.Abstraction.SymbolList;

    /// <summary>
    /// Result from the NASDAQ symbols query.
    /// </summary>
    public class SymbolListResult : ISymbolListResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SymbolListResult"/> class.
        /// </summary>
        /// <param name="symbols">List of symbols.</param>
        /// <param name="fileCreationTime">Symbol file creation time.</param>
        public SymbolListResult(IEnumerable<SymbolItem> symbols, DateTime fileCreationTime)
        {
            this.Symbols = symbols;
            this.FileCreationTime = fileCreationTime;
        }

        /// <summary>
        /// Gets NASDAQ symbols file creation time.
        /// </summary>
        public DateTime FileCreationTime { get; private set; }

        /// <summary>
        /// Gets list of items.
        /// </summary>
        public IEnumerable<ISymbolListItem> Symbols { get; private set; }

        /// <summary>
        /// Symbol Item.
        /// </summary>
        public class SymbolItem : ISymbolListItem
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SymbolItem"/> class.
            /// </summary>
            /// <param name="symbol">Identifier for the security.</param>
            /// <param name="securityName">Company issuing the security.</param>
            /// <param name="exchange">Listing stock exchange or market of the security.</param>
            internal SymbolItem(string symbol, string securityName, string exchange)
            {
                this.Symbol = symbol;
                this.SecurityName = securityName;
                this.Exchange = exchange;
            }

            /// <summary>
            /// Gets the identifier for the security.
            /// </summary>
            public string Symbol { get; private set; }

            /// <summary>
            /// Gets company issuing the security.
            /// </summary>
            public string SecurityName { get; private set; }

            /// <summary>
            /// Gets the listing stock exchange or market of the security.
            /// </summary>
            public string Exchange { get; private set; }
        }
    }
}