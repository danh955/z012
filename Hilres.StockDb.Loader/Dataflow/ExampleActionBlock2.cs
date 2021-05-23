// <copyright file="ExampleActionBlock2.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader.Dataflow
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Example action block class.
    /// </summary>
    public class ExampleActionBlock2 : ActionBlockWorker<ExampleActionBlock2.Input>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleActionBlock2"/> class.
        /// </summary>
        /// <param name="service">DataflowService.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        public ExampleActionBlock2(DataflowService service, CancellationToken cancellationToken)
            : base(service, cancellationToken)
        {
        }

        /// <inheritdoc/>
        protected override async Task ActionTask(Input data)
        {
            if (!this.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(0, this.CancellationToken);
            }
        }

        /// <summary>
        /// Input class for ExampleActionBlock2 action block.
        /// </summary>
        public class Input
        {
            /// <summary>
            /// Gets or sets iD.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Gets or sets name of something.
            /// </summary>
            public string Name { get; set; }
        }
    }
}