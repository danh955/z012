// <copyright file="IStockDataSource.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater.Abstraction
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Updater.Abstraction.SymbolList;

    /// <summary>
    /// Financial data service interface.
    /// </summary>
    public interface IStockDataSource
    {
        /// <summary>
        /// Get all the symbols.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>NasdaqSymbolsResult.</returns>
        public Task<ISymbolListResult> GetSymbleList(CancellationToken cancellationToken);
    }
}