// <copyright file="ScreenerTableQueryHandler.cs" company="None">
// Free and open source code.
// </copyright>
namespace MT.BlazorUi.Application.Screener
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    /// <summary>
    /// Screener table query request handler class.
    /// </summary>
    public class ScreenerTableQueryHandler : IRequestHandler<ScreenerTableQuery, ScreenerTableQueryResult>
    {
        /// <inheritdoc/>
        public async Task<ScreenerTableQueryResult> Handle(ScreenerTableQuery request, CancellationToken cancellationToken)
        {
            //// TODO: Implement handler.
            await Task.Delay(1, cancellationToken);

            return new ScreenerTableQueryResult();
        }
    }
}