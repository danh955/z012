// <copyright file="StartStopLoaderServiceButton.razor.cs" company="None">
// Free and open source code.
// </copyright>
namespace MT.BlazorUi.Features.Status
{
    using System;
    using System.Threading.Tasks;
    using Hilres.Stock.Updater;
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Start stop loader service button class.
    /// </summary>
    public partial class StartStopLoaderServiceButton : ComponentBase, IDisposable
    {
        /// <summary>
        /// Gets or sets stock database loader service.
        /// </summary>
        [Inject]
        protected LoaderBackgroundStatus LoaderStatus { get; set; }

        private bool StartStopDisabled => this.LoaderStatus.State == LoaderRunState.Starting
                                       || this.LoaderStatus.State == LoaderRunState.Stopping;

        private string StartStopText => this.LoaderStatus.IsRunning ? "STOP" : "START";

        /// <inheritdoc/>
        public void Dispose()
        {
            if (this.LoaderStatus != null)
            {
                this.LoaderStatus.OnPropertyChangedAsync -= this.Refresh;
                this.LoaderStatus = null;
                GC.SuppressFinalize(this);
            }
        }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            this.LoaderStatus.OnPropertyChangedAsync += this.Refresh;
        }

        private async Task Refresh(string propertyName, object oldValue)
        {
            if (propertyName == nameof(this.LoaderStatus.State))
            {
                await this.InvokeAsync(this.StateHasChanged);
            }
        }

        private void StartStopAction()
        {
            if (this.LoaderStatus.IsRunning)
            {
                this.LoaderStatus.Mode = LoaderRunMode.Stop;
            }
            else
            {
                this.LoaderStatus.Mode = LoaderRunMode.Run;
            }
        }
    }
}