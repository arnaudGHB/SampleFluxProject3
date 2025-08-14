using Microsoft.EntityFrameworkCore;

namespace CBS.PortfolioManagement.Domain
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }

        // DbSets for PortfolioManagement entities will be added here later.
        // For example:
        // public DbSet<Investment> Investments { get; set; }
        // public DbSet<Asset> Assets { get; set; }
    }
}
