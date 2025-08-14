using CBS.CheckManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.CheckManagement.Domain
{
    /// <summary>
    /// The database context for the Check Management microservice.
    /// </summary>
    public class CheckManagementContext : DbContext
    {
        public CheckManagementContext(DbContextOptions<CheckManagementContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Ping entities.
        /// </summary>
        public DbSet<Ping> Pings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure the Ping entity
            modelBuilder.Entity<Ping>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Message).HasMaxLength(500);
                entity.Property(e => e.Timestamp).IsRequired();
                entity.Property(e => e.CreatedDate).IsRequired();
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            });
        }
    }
}
