// <copyright file="UpdateDbWorker.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader.Workers
{
    using System.Threading.Tasks;

    /// <summary>
    /// Update database worker class.
    /// This is the class that manages loading or updating any symbol or prices data.
    /// </summary>
    public class UpdateDbWorker
    {
        /// <summary>
        /// Update database.
        /// </summary>
        /// <returns>Task.</returns>
        public async Task UpdateDatabase()
        {
            await Task.Delay(1000);
        }
    }
}