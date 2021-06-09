// <copyright file="UpdateStockDataFromDataSource.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System.Threading.Tasks;

    /// <summary>
    /// Update database worker class.
    /// This is the class that manages loading or updating any symbol or prices data.
    /// </summary>
    public class UpdateStockDataFromDataSource
    {
        /// <summary>
        /// Do the update to the database.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task DoUpdate()
        {
            await Task.Delay(1000);
        }
    }
}