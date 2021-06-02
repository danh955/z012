// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add stock database loader service to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddStockDbLoaderService(this IServiceCollection service)
        {
            service.AddSingleton<LoaderBackgroundStatus>();
            service.AddHostedService<LoaderBackgroundService>();
            return service;
        }
    }
}