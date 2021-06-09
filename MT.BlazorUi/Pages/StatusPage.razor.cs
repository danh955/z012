// <copyright file="StatusPage.razor.cs" company="None">
// Free and open source code.
// </copyright>

namespace MT.BlazorUi.Pages
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Hilres.Stock.Updater;
    using Microsoft.AspNetCore.Components;

    /// <summary>
    /// Status page class.
    /// </summary>
    public partial class StatusPage : ComponentBase, IDisposable
    {
        /// <summary>
        /// Gets or sets stock database loader service.
        /// </summary>
        [Inject]
        protected LoaderBackgroundStatus LoaderStatus { get; set; }

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
            if (new[]
                {
                    nameof(this.LoaderStatus.State),
                    nameof(this.LoaderStatus.Count),
                }.Contains(propertyName))
            {
                await this.InvokeAsync(this.StateHasChanged);
            }
        }
    }
}