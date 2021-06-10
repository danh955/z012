// <copyright file="UpdateStockDataFromDataSource.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Repository;
    using Hilres.Stock.Updater.Abstraction;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Update database worker class.
    /// This is the class that manages loading or updating any symbol or prices data.
    /// </summary>
    public class UpdateStockDataFromDataSource
    {
        private readonly IDbContextFactory<StockDbContext> dbContextFactory;
        private readonly IStockDataSource stockDataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStockDataFromDataSource"/> class.
        /// </summary>
        /// <param name="stockDataSource">IStockDataSource.</param>
        /// <param name="dbContextFactory">Database context factory for the stock.</param>
        public UpdateStockDataFromDataSource(IStockDataSource stockDataSource, IDbContextFactory<StockDbContext> dbContextFactory)
        {
            this.stockDataSource = stockDataSource;
            this.dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Do the update to the database.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        public async Task DoUpdate(CancellationToken cancellationToken)
        {
            await this.UpdateSymbols(cancellationToken);
        }

        /// <summary>
        /// Update the symbols in the database.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        private async Task UpdateSymbols(CancellationToken cancellationToken)
        {
            var db = this.dbContextFactory.CreateDbContext();

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