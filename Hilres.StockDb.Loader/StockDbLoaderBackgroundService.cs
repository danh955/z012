// <copyright file="StockDbLoaderBackgroundService.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using Hilres.StockDb.Loader.Dataflow;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Stock database loader background service class.
    /// </summary>
    public class StockDbLoaderBackgroundService : BackgroundService
    {
        private bool isRunning = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockDbLoaderBackgroundService"/> class.
        /// </summary>
        /// <param name="status">StockDbLoaderBackgroundStatus.</param>
        public StockDbLoaderBackgroundService(StockDbLoaderBackgroundStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Gets background status.
        /// </summary>
        internal StockDbLoaderBackgroundStatus Status { get; init; }

        /// <inheritdoc/>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            this.Status.OnPropertyChangedAsync += this.Refresh;
            await base.StartAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (this.Status != null)
            {
                this.Status.OnPropertyChangedAsync -= this.Refresh;
            }

            await base.StopAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DataflowService dataflow = new(stoppingToken);
            await dataflow.ExampleActionBlock1.Action.SendAsync(new() { Id = 1 });

            while (!stoppingToken.IsCancellationRequested)
            {
                if (this.isRunning)
                {
                    this.Status.State = StockDbLoaderState.Running;
                    this.Status.Count++;
                }
                else
                {
                    this.Status.State = StockDbLoaderState.Stopped;
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Refresh(string propertyName, object oldValue)
        {
            if (propertyName == nameof(this.Status.Mode))
            {
                switch (this.Status.Mode)
                {
                    case StockDbLoaderMode.Run:
                        this.Status.State = StockDbLoaderState.Starting;
                        this.isRunning = true;
                        await Task.Delay(0);
                        break;

                    case StockDbLoaderMode.Stop:
                        this.Status.State = StockDbLoaderState.Stopping;
                        this.isRunning = false;
                        break;
                }
            }
        }
    }
}