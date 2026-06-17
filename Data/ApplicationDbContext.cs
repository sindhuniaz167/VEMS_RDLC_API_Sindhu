using Microsoft.EntityFrameworkCore;
using VEMS_RDLC_API.Models;

namespace VEMS_RDLC_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for tables (if you need them)
        public DbSet<ChallanData> ChallanData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ChallanData for Stored Procedure
            modelBuilder.Entity<ChallanData>()
                .HasNoKey()
                .ToView(null); // Not a real table/view

            // Configure property mappings
            modelBuilder.Entity<ChallanData>()
                .Property(e => e.Detail1_FeeHeadID)
                .HasColumnName("Detail1_FeeHeadID");

            modelBuilder.Entity<ChallanData>()
                .Property(e => e.Detail2_FeeHeadID)
                .HasColumnName("Detail2_FeeHeadID");

            // Add more mappings as needed
        }
    }
}