// <copyright file="LoaderBackgroundService.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Updater
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;
    using Hilres.Stock.Updater.Workers;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Stock database loader background service class.
    /// </summary>
    internal class LoaderBackgroundService : BackgroundService
    {
        private bool isRunning = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoaderBackgroundService"/> class.
        /// </summary>
        /// <param name="status">StockDbLoaderBackgroundStatus.</param>
        public LoaderBackgroundService(LoaderBackgroundStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Gets background status.
        /// </summary>
        internal LoaderBackgroundStatus Status { get; init; }

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
                    this.Status.State = LoaderRunState.Running;
                    this.Status.Count++;
                }
                else
                {
                    this.Status.State = LoaderRunState.Stopped;
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
                    case LoaderRunMode.Run:
                        this.Status.State = LoaderRunState.Starting;
                        this.isRunning = true;
                        await Task.Delay(0);
                        break;

                    case LoaderRunMode.Stop:
                        this.Status.State = LoaderRunState.Stopping;
                        this.isRunning = false;
                        break;
                }
            }
        }
    }
}