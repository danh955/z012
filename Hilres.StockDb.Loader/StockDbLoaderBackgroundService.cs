// <copyright file="StockDbLoaderBackgroundService.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Loader
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Stock database loader background service class.
    /// </summary>
    public class StockDbLoaderBackgroundService : BackgroundService
    {
        private readonly StockDbLoaderBackgroundStatus status;
        private bool isRunning = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockDbLoaderBackgroundService"/> class.
        /// </summary>
        /// <param name="status">StockDbLoaderBackgroundStatus.</param>
        public StockDbLoaderBackgroundService(StockDbLoaderBackgroundStatus status)
        {
            this.status = status;
        }

        /// <inheritdoc/>
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            this.status.OnPropertyChangedAsync += this.Refresh;
            await base.StartAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (this.status != null)
            {
                this.status.OnPropertyChangedAsync -= this.Refresh;
            }

            await base.StopAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (this.isRunning)
                {
                    this.status.State = StockDbLoaderState.Running;
                    this.status.Count++;
                }
                else
                {
                    this.status.State = StockDbLoaderState.Stopped;
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Refresh(string propertyName, object oldValue)
        {
            if (propertyName == nameof(this.status.Mode))
            {
                switch (this.status.Mode)
                {
                    case StockDbLoaderMode.Run:
                        this.status.State = StockDbLoaderState.Starting;
                        this.isRunning = true;
                        await Task.Delay(0);
                        break;

                    case StockDbLoaderMode.Stop:
                        this.status.State = StockDbLoaderState.Stopping;
                        this.isRunning = false;
                        break;
                }
            }
        }
    }
}