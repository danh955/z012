// <copyright file="YahooFinancialDataService.SymbolList.cs" company="None">
// Free and open source code.
// </copyright>
#pragma warning disable SA1118 // Parameter should not span multiple lines

namespace Hilres.Stock.DataSource.Yahoo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Hilres.Stock.Updater.Abstraction.SymbolList;

    /// <summary>
    /// Yahoo financial data service class for SymbolList.
    /// </summary>
    public partial class StockDataSourceYahoo
    {
        private const string FileCreationTimeText = @"File Creation Time:";
        private const string NasdaqListedUri = @"http://www.nasdaqtrader.com/dynamic/SymDir/nasdaqlisted.txt";
        private const string OtherListedUri = @"http://www.nasdaqtrader.com/dynamic/SymDir/otherlisted.txt";

        /// <summary>
        /// Get all the symbols.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>NasdaqSymbolsResult.</returns>
        public async Task<ISymbolListResult> GetSymbleList(CancellationToken cancellationToken)
        {
            var csvConfigurationPipe = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "|" };

            var nasdaqSymbolTask = this.GetItemsAsync(
                   uri: NasdaqListedUri,
                   csvConfiguration: csvConfigurationPipe,
                   cancellationToken: cancellationToken,
                   createItem: (csv) =>
                   {
                       // Is this a test symbol?
                       if (Parse.IsTrue(csv.GetField(3)))
                       {
                           return null;
                       }

                       return new(
                           symbol: csv[0].Trim(),
                           securityName: csv[1].Trim(),
                           exchange: "NASDAQ");
                   });

            var otherSymbolTask = this.GetItemsAsync(
               uri: OtherListedUri,
               csvConfiguration: csvConfigurationPipe,
               cancellationToken: cancellationToken,
               createItem: (csv) =>
               {
                   // Is this a test symbol?
                   if (Parse.IsTrue(csv.GetField(6)))
                   {
                       return null;
                   }

                   return new(
                       symbol: csv[7].Trim(),
                       securityName: csv[1].Trim(),
                       exchange: csv[2].ToUpper() switch
                       {
                           "A" => "NYSE MKT",
                           "N" => "New York Stock Exchange(NYSE)",
                           "P" => "NYSE ARCA",
                           "Z" => "BATS Global Markets(BATS)",
                           "V" => "Investors' Exchange, LLC (IEXG)",
                           _ => csv[2],
                       });
               });

            await Task.WhenAll(nasdaqSymbolTask, otherSymbolTask);

            return new SymbolListResult(
                symbols: nasdaqSymbolTask.Result.Item1.Union(otherSymbolTask.Result.Item1),
                fileCreationTime: Max(nasdaqSymbolTask.Result.Item2, otherSymbolTask.Result.Item2));
        }

        private static DateTime Max(DateTime value1, DateTime value2)
        {
            return value1 > value2 ? value1 : value2;
        }

        /// <summary>
        /// Get a list of items.
        /// </summary>
        /// <param name="uri">URL to get the data.</param>
        /// <param name="csvConfiguration">CSV configuration.</param>
        /// <param name="createItem">Function to create item.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        /// <returns>List of items.</returns>
        private async Task<Tuple<IEnumerable<SymbolListResult.SymbolItem>, DateTime>> GetItemsAsync(
            string uri,
            CsvConfiguration csvConfiguration,
            Func<CsvReader, SymbolListResult.SymbolItem> createItem,
            CancellationToken cancellationToken)
        {
            List<SymbolListResult.SymbolItem> items = new();
            DateTime fileCreationTime = default;

            HttpClient httpClient = this.clientFactory.CreateClient();
            using var responseStream = await httpClient.GetStreamAsync(uri, cancellationToken);
            using var streamReader = new StreamReader(responseStream);

            using var csv = new CsvReader(streamReader, csvConfiguration);

            if (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
            {
                csv.ReadHeader();

                while (!cancellationToken.IsCancellationRequested && await csv.ReadAsync())
                {
                    if (csv[0].StartsWith(FileCreationTimeText))
                    {
                        fileCreationTime = Parse.FileCreationTime(csv[0][FileCreationTimeText.Length..]);
                    }
                    else
                    {
                        var newItem = createItem(csv);
                        if (newItem != null)
                        {
                            items.Add(newItem);
                        }
                    }
                }
            }

            return new(items, fileCreationTime);
        }
    }
}