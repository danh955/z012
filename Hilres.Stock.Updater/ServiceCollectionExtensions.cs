// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the stock database updater to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddHilresStockUpdater(this IServiceCollection service)
        {
            service.AddTransient<UpdateStockDataFromDataSource>();
            service.AddSingleton<LoaderBackgroundStatus>();
            service.AddHostedService<LoaderBackgroundService>();
            return service;
        }
    }
}