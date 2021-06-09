// <copyright file="DataflowService.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater.Workers
{
    using System.Threading;

    /// <summary>
    /// Data flow service class.
    /// </summary>
    public class DataflowService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataflowService"/> class.
        /// </summary>
        /// <param name="cancellationToken">CancellationToken.</param>
        public DataflowService(CancellationToken cancellationToken)
        {
            this.ExampleActionBlock1 = new(this, cancellationToken);
            this.ExampleActionBlock2 = new(this, cancellationToken);
        }

        /// <summary>
        /// Gets example action block #1.
        /// </summary>
        internal ExampleActionBlock1 ExampleActionBlock1 { get; init; }

        /// <summary>
        /// Gets example action block #2.
        /// </summary>
        internal ExampleActionBlock2 ExampleActionBlock2 { get; init; }
    }
}