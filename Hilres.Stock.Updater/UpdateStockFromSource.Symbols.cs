// <copyright file="UpdateStockFromSource.Symbols.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Repository;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Update data for symbols partial class.
    /// </summary>
    public partial class UpdateStockFromSource
    {
        /// <summary>
        /// Update the symbols in the database.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        private async Task UpdateDataSymbols(CancellationToken cancellationToken)
        {
            using var db = this.dbContextFactory.CreateDbContext();

            // Get both at the same time.
            var resultDataTask = this.stockDataSource.GetSymbleList(cancellationToken);
            var dbStocksTask = db.Stocks.Select(s => s).ToListAsync(cancellationToken);

            // Wait on the database.
            var dbStocks = await dbStocksTask;
            Dictionary<string, StockEntity> stocks = dbStocks.ToDictionary(s => s.Symbol);

            // Wait on the data source.
            var resultData = await resultDataTask;
            var sourceStocks = resultData.Symbols
                                .Where(d => d.Symbol.Trim().Length > 0 && char.IsLetter(d.Symbol.Trim()[^1]))
                                .Select(d => new StockEntity
                                {
                                    Symbol = d.Symbol.Trim().ToUpper(),
                                    Name = d.SecurityName.Trim(),
                                    Exchange = d.Exchange.Trim(),
                                });

            var newStocks = sourceStocks
                            .Where(d => !stocks.ContainsKey(d.Symbol.Trim().ToUpper()));

            db.Stocks.AddRange(newStocks.Distinct());
            await db.SaveChangesAsync(cancellationToken);
        }
    }
}