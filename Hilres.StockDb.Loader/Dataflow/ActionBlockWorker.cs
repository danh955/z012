// <copyright file="ActionBlockWorker.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader.Dataflow
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    /// <summary>
    /// Action block worker class.
    /// </summary>
    /// <typeparam name="TInput">Specifies the type of data to post to the target.</typeparam>
    public abstract class ActionBlockWorker<TInput>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionBlockWorker{TInput}"/> class.
        /// </summary>
        /// <param name="service">DataflowService.</param>
        /// <param name="cancellationToken">CancellationToken.</param>
        public ActionBlockWorker(DataflowService service, CancellationToken cancellationToken)
        {
            this.Service = service;
            this.CancellationToken = cancellationToken;

            ExecutionDataflowBlockOptions options = new()
            {
                CancellationToken = cancellationToken,
            };

            this.Action = new(this.ActionTask, options);
        }

        /// <summary>
        /// Gets action block that is used.
        /// </summary>
        public ActionBlock<TInput> Action { get; init; }

        /// <summary>
        /// Gets cancellationToken.
        /// </summary>
        protected CancellationToken CancellationToken { get; init; }

        /// <summary>
        /// Gets dataflowService.
        /// </summary>
        protected DataflowService Service { get; init; }

        /// <summary>
        /// Action task to process each item from the queue.
        /// </summary>
        /// <param name="data">Input data.</param>
        /// <returns>Task.</returns>
        protected abstract Task ActionTask(TInput data);
    }
}