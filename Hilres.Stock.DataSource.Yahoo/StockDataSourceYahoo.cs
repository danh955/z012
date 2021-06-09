// <copyright file="YahooFinancialDataService.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.DataSource.Yahoo
{
    using System.Net.Http;
    using Hilres.Stock.Updater.Abstraction;

    /// <summary>
    /// Yahoo financial data service class.
    /// </summary>
    public partial class StockDataSourceYahoo : IStockDataSource
    {
        private readonly IHttpClientFactory clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockDataSourceYahoo"/> class.
        /// </summary>
        /// <param name="clientFactory">IHttpClientFactory.</param>
        public StockDataSourceYahoo(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
    }
}