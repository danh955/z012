// <copyright file="ScreenerTableQueryResultRow.cs" company="None">
// Free and open source code.
// </copyright>
namespace MT.BlazorUi.Application.Screener
{
    using System.Collections.Generic;

    /// <summary>
    /// Screener table query result row class.
    /// </summary>
    public class ScreenerTableQueryResultRow
    {
        /// <summary>
        /// Gets stock symbol for row.
        /// </summary>
        public string Symbol { get; internal set; }

        /// <summary>
        /// Gets a list of percentages for the row.
        /// </summary>
        public IEnumerable<double?> Percentages { get; internal set; }
    }
}