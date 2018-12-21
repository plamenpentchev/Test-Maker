using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestMakerFreeWebApp.Data.WFSSDTest
{
    public partial class WFSSDTestContext : DbContext
    {
        public WFSSDTestContext()
        {
        }

        public WFSSDTestContext(DbContextOptions<WFSSDTestContext> options)
            : base(options)
        {
        }

        // Unable to generate entity type for table 'dbo.stammdaten'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.stammdaten_abwesenheiten'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ICTS-SQL12;Database=WFSSD-Test;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
            modelBuilder.Entity<Stammdaten>( entity => 
            {
                entity.ToTable("stammdaten");
            });
        }
    }
}
