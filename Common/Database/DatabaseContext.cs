using AcraBackend.Common.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace AcraBackend.Common.Database
{
    public class DatabaseContext : DbContext
    {
        public static readonly string DatabaseFile = "../reports.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=" + DatabaseFile);

        public DbSet<FatalReport> FatalReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
