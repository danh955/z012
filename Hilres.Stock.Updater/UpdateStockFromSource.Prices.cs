// <copyright file="UpdateStockFromSource.Prices.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Update data for stock prices partial class.
    /// </summary>
    public partial class UpdateStockFromSource
    {
        /// <summary>
        /// Create an action block for updating prices.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>ActionBlock./returns>
        private ActionBlock<int> CreateUpdatePriceWorker(CancellationToken cancellationToken)
        {
            return new(this.UpdatePriceWorkerAction,
                        new ExecutionDataflowBlockOptions()
                        {
                            CancellationToken = cancellationToken,
                        });
        }

        /// <summary>
        /// This will update all the stock price.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        private async Task UpdateAllPrices(CancellationToken cancellationToken)
        {
            var stockIds = await this.GetAllStockIdsAsync(cancellationToken);
            stockIds.ForEach(id => this.updatePriceWorker.SendAsync(id, cancellationToken));
        }

        /// <summary>
        /// Get all the stock symbol IDs.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        private async Task<List<int>> GetAllStockIdsAsync(CancellationToken cancellationToken)
        {
            using var db = this.dbContextFactory.CreateDbContext();
            return await db.Stocks.Select(s => s.Id).Take(3).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Action block worker task that will update a single stock prices.
        /// </summary>
        /// <param name="stockId">Stock ID to update prices.</param>
        /// <returns>Task.</returns>
        private async Task UpdatePriceWorkerAction(int stockId)
        {
            this.logger.LogDebug("PriceUpdateAction({0})", stockId);
            await Task.Delay(1);
        }
    }
}