// <copyright file="ScreenerTable.razor.cs" company="None">
// Free and open source code.
// </copyright>

namespace MT.BlazorUi.Features.Screener
{
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Components;
    using MT.BlazorUi.Application.Screener;

    /// <summary>
    /// Screener table code behind class.
    /// </summary>
    public partial class ScreenerTable : ComponentBase
    {
        /// <summary>
        /// Gets or sets mediator.
        /// </summary>
        [Inject]
        protected IMediator Mediator { get; set; }

        /// <summary>
        /// Gets or sets result from screen query.
        /// </summary>
        private ScreenerTableQueryResult ScreenerResult { get; set; }

        /// <inheritdoc/>
        protected override async Task OnInitializedAsync()
        {
            this.ScreenerResult = await this.Mediator.Send(new ScreenerTableQuery());
            await base.OnInitializedAsync();
        }
    }
}