// <copyright file="IWeatherService.cs" company="None">
// Free and open source code.
// </copyright>
namespace ConsoleAppTPL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Weather service interface.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Get five day temperatures.
        /// </summary>
        /// <returns>List of temperatures.</returns>
        public Task<IReadOnlyList<int>> GetFiveDayTemperaturesAsync();
    }
}