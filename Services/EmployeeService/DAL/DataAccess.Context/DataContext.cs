using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
