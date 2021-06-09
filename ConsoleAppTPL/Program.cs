// <copyright file="Program.cs" company="None">
// Free and open source code.
// </copyright>
namespace ConsoleAppTPL
{
    using System.Threading.Tasks;
    using Hilres.Stock.DataSource.Yahoo;
    using Hilres.Stock.Repository;
    using Hilres.Stock.Updater;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main program class.
    /// <!-- https://dfederm.com/building-a-console-app-with-.net-generic-host -->
    /// </summary>
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext.Configuration.GetConnectionString("StockDatabase");
                    services.AddPooledDbContextFactory<StockDbContext>(options => options.UseSqlServer(connectionString));
                    services.AddHilresStockDataSourceYahoo();
                    services.AddHilresStockUpdater();

                    services.AddHostedService<ConsoleHostedService>();
                    services.AddSingleton<IWeatherService, WeatherService>();
                    services.AddOptions<WeatherSettings>().Bind(hostContext.Configuration.GetSection("Weather"));
                })
                .RunConsoleAsync();
        }
    }
}