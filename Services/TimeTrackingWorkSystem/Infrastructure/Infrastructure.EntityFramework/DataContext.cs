using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.EntityFramework
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Project> Project { get; set; }
		public DbSet<TimeTracker> TimeTracker { get; set; }
		public DbSet<Employee> Employee { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Project>()
				.HasMany(u => u.TimeTrackers)
				.WithOne(c => c.Project)
				.IsRequired();

			modelBuilder.Entity<Project>().Property(c => c.Name).HasMaxLength(100);
			modelBuilder.Entity<TimeTracker>().Property(c => c.Description).HasMaxLength(250);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
		}
	}
}
