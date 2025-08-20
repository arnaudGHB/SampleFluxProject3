using Microsoft.EntityFrameworkCore;

namespace CBS.CheckManagementManagement.Domain.Context
{
    public class CheckManagementContext : DbContext
    {
        public CheckManagementContext(DbContextOptions<CheckManagementContext> options) : base(options)
        {
        }

        public DbSet<CBS.CheckManagementManagement.Data.Entity.Ping> Pings { get; set; }

        // public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
