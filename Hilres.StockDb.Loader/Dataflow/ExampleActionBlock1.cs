// <copyright file="ExampleActionBlock1.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader.Dataflow
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    /// <summary>
    /// Example action block class.
    /// </summary>
    public class ExampleActionBlock1 : ActionBlockWorker<ExampleActionBlock1.Input>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleActionBlock1"/> class.
        /// </summary>
        /// <param name="service">DataflowService.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        public ExampleActionBlock1(DataflowService service, CancellationToken cancellationToken)
            : base(service, cancellationToken)
        {
        }

        /// <inheritdoc/>
        protected override async Task ActionTask(Input data)
        {
            if (!this.CancellationToken.IsCancellationRequested)
            {
                await this.Service.ExampleActionBlock2.Action.SendAsync(new() { Id = data.Id, Name = "something" });
            }
        }

        /// <summary>
        /// Input class for ExampleActionBlock1 action block.
        /// </summary>
        public class Input
        {
            /// <summary>
            /// Gets or sets iD.
            /// </summary>
            public int Id { get; set; }
        }
    }
}