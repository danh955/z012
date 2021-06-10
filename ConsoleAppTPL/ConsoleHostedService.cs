// <copyright file="ConsoleHostedService.cs" company="None">
// Free and open source code.
// </copyright>
namespace ConsoleAppTPL
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hilres.Stock.Repository;
    using Hilres.Stock.Updater;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Console hosted service class.
    /// </summary>
    public class ConsoleHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime appLifetime;
        private readonly IDbContextFactory<StockDbContext> stockDbContextFactory;
        private readonly ILogger logger;
        private readonly IWeatherService weatherService;
        private readonly UpdateStockDataFromDataSource updateFinancialDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleHostedService"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="appLifetime">Application lifetime.</param>
        /// <param name="weatherService">Weather service.</param>
        /// <param name="stockDbContextFactory">IDbContextFactory for StockDbContext.</param>
        /// <param name="updateFinancialDataService">UpdateFinancialDataService.</param>
        public ConsoleHostedService(
            ILogger<ConsoleHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IWeatherService weatherService,
            IDbContextFactory<StockDbContext> stockDbContextFactory,
            UpdateStockDataFromDataSource updateFinancialDataService)
        {
            this.logger = logger;
            this.appLifetime = appLifetime;
            this.weatherService = weatherService;
            this.stockDbContextFactory = stockDbContextFactory;
            this.updateFinancialDataService = updateFinancialDataService;
        }

        /// <summary>
        /// Start.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await this.EnsureDatabaseIsCreated(cancellationToken);

                // Update the database with new stock data.
                await this.updateFinancialDataService.DoUpdate(cancellationToken);

                IReadOnlyList<int> temperatures = await this.weatherService.GetFiveDayTemperaturesAsync();
                for (int i = 0; i < temperatures.Count; i++)
                {
                    this.logger.LogInformation($"{DateTime.Today.AddDays(i).DayOfWeek}: {temperatures[i]}");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Unhandled exception!");
            }
            finally
            {
                // Stop the application once the work is done
                this.appLifetime.StopApplication();
            }
        }

        /// <summary>
        /// Stop.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>Task.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogDebug("Exiting");
            return Task.CompletedTask;
        }

        private async Task EnsureDatabaseIsCreated(CancellationToken cancellationToken)
        {
            using var context = this.stockDbContextFactory.CreateDbContext();
            await context.Database.EnsureCreatedAsync(cancellationToken);
            //// await context.Database.MigrateAsync(cancellationToken);
        }
    }
}