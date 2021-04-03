// <copyright file="ScreenerTableQueryResult.cs" company="None">
// Free and open source code.
// </copyright>
namespace MT.BlazorUi.Application.Screener
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Screener table query result class.
    /// </summary>
    public class ScreenerTableQueryResult
    {
        /// <summary>
        /// Gets headings for the percentages.
        /// </summary>
        public IEnumerable<DateTime> Headings { get; internal set; }

        /// <summary>
        /// Gets list of rows.
        /// </summary>
        public IEnumerable<ScreenerTableQueryResultRow> Rows { get; internal set; }
    }
}