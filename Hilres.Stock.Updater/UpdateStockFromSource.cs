// <copyright file="UpdateStockFromSource.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Repository;
    using Hilres.Stock.Updater.Abstraction;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Update database worker class.
    /// This is the class that manages loading or updating any symbol or prices data.
    /// </summary>
    public partial class UpdateStockFromSource
    {
        private readonly IDbContextFactory<StockDbContext> dbContextFactory;
        private readonly IStockDataSource stockDataSource;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateStockFromSource"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="stockDataSource">IStockDataSource.</param>
        /// <param name="dbContextFactory">Database context factory for the stock.</param>
        public UpdateStockFromSource(
            ILogger<UpdateStockFromSource> logger,
            IStockDataSource stockDataSource,
            IDbContextFactory<StockDbContext> dbContextFactory)
        {
            this.logger = logger;
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
            await this.UpdateDataSymbols(cancellationToken);
            await this.UpdateDataPrices(cancellationToken);
        }
    }
}