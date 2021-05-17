// <copyright file="ServiceCollectionExtensions.cs" company="None">
// Free and open source code.
// </copyright>
namespace MT.BlazorUi.Application
{
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Service collection extensions class.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add BlazorUi application project to the service collection.
        /// </summary>
        /// <param name="service">IServiceCollection.</param>
        /// <returns>Updated IServiceCollection.</returns>
        public static IServiceCollection AddBlazorUiApplication(this IServiceCollection service)
        {
            service.AddMediatR(typeof(ServiceCollectionExtensions));
            return service;
        }
    }
}