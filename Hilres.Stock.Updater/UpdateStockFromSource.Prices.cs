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
        private async Task UpdateDataPrices(CancellationToken cancellationToken)
        {
            ActionBlock<int> updatePriceAction = new(
                                this.PriceUpdateAction,
                                new ExecutionDataflowBlockOptions()
                                {
                                    CancellationToken = cancellationToken,
                                });

            var stockIds = await this.GetStockIdsAsync(cancellationToken);
            stockIds.ForEach(id => updatePriceAction.SendAsync(id, cancellationToken));

            updatePriceAction.Complete();
            await updatePriceAction.Completion;
        }

        private async Task<List<int>> GetStockIdsAsync(CancellationToken cancellationToken)
        {
            using var db = this.dbContextFactory.CreateDbContext();
            return await db.Stocks.Select(s => s.Id).Take(3).ToListAsync(cancellationToken);
        }

        private async Task PriceUpdateAction(int stockId)
        {
            this.logger.LogDebug("PriceUpdateAction({0})", stockId);
            await Task.Delay(1);
        }
    }
}