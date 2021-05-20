// <copyright file="StockDbLoaderState.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.StockDb.Loader
{
    /// <summary>
    /// State of the stock database loader.
    /// </summary>
    public enum StockDbLoaderState
    {
        /// <summary>
        /// Starting.
        /// </summary>
        Starting,

        /// <summary>
        /// Running.
        /// </summary>
        Running,

        /// <summary>
        /// Stopping.
        /// </summary>
        Stopping,

        /// <summary>
        /// Stopped.
        /// </summary>
        Stopped,
    }
}