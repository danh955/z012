// <copyright file="StockDbContext.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.StockDb.Repository
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Entity database context interface.
    /// </summary>
    public class StockDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockDbContext"/> class.
        /// </summary>
        /// <param name="options">DbContextOptions.</param>
        public StockDbContext(DbContextOptions<StockDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the stock database entity.
        /// </summary>
        public DbSet<StockEntity> Stocks { get; set; }

        /// <summary>
        /// Gets or sets the stock price database entity.
        /// </summary>
        public DbSet<StockPriceEntity> StockPrices { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
        }
    }
}