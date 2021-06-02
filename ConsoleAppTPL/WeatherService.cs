// <copyright file="WeatherService.cs" company="None">
// Free and open source code.
// </copyright>

namespace ConsoleAppTPL
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Weather service class.
    /// </summary>
    internal sealed class WeatherService : IWeatherService
    {
        private readonly IOptions<WeatherSettings> weatherSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService"/> class.
        /// </summary>
        /// <param name="weatherSettings">Weather service settings.</param>
        public WeatherService(IOptions<WeatherSettings> weatherSettings)
        {
            this.weatherSettings = weatherSettings;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<int>> GetFiveDayTemperaturesAsync()
        {
            int[] temperatures = new[] { 76, 76, 77, 79, 78 };
            if (this.weatherSettings.Value.Unit.Equals("C", StringComparison.OrdinalIgnoreCase))
            {
                for (int i = 0; i < temperatures.Length; i++)
                {
                    temperatures[i] = (int)Math.Round((temperatures[i] - 32) / 1.8);
                }
            }

            await Task.Delay(1000);
            return await Task.FromResult<IReadOnlyList<int>>(temperatures);
        }
    }
}