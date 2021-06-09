// <copyright file="LoaderRunState.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.Stock.Updater
{
    /// <summary>
    /// State of the stock database loader.
    /// </summary>
    public enum LoaderRunState
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