using Microsoft.EntityFrameworkCore;
using CBS.CheckManagement.Domain.Entities;
using CBS.CheckManagement.Data.Entity;

namespace CBS.CheckManagement.Data
{
    public class CheckManagementContext : DbContext
    {
        public CheckManagementContext(DbContextOptions<CheckManagementContext> options) : base(options)
        {
        }

        // DbSet properties for your entities
        public DbSet<Ping> Pings { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure entity mappings here if needed
            modelBuilder.Entity<Ping>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).IsRequired();
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.Url).IsRequired();
                entity.Property(e => e.ControllerName).IsRequired();
                entity.Property(e => e.ActionName).IsRequired();
                entity.Property(e => e.LogLevel).IsRequired();
                entity.Property(e => e.Message).IsRequired();
            });
        }
    }
}
