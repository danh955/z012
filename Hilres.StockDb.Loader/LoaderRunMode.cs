// <copyright file="LoaderRunMode.cs" company="None">
// Free and open source code.
// </copyright>

namespace Hilres.StockDb.Loader
{
    /// <summary>
    /// Stock database loader mode.
    /// </summary>
    public enum LoaderRunMode
    {
        /// <summary>
        /// Run. The background task is running or to start..
        /// </summary>
        Run,

        /// <summary>
        /// Stop. The background task is stopped or to be stopped.
        /// </summary>
        Stop,
    }
}