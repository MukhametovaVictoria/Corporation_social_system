using DA.Entities;
using Microsoft.EntityFrameworkCore;
namespace DA.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Accomplishment> Accomplishment { get; set; }
        public DbSet<Communication> Communication { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Skill> Skill { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Employee
            modelBuilder.Entity<Employee>()
                .HasMany(emp => emp.AccomplishmentsList)
                .WithOne(accomplishment => accomplishment.Employee)
                .HasForeignKey(accomplishment => accomplishment.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(com => com.CommunicationsList)
                .WithOne(nc => nc.Employee)
                .HasForeignKey(nc => nc.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ExperienceList)
                .WithOne(nc => nc.Employee)
                .HasForeignKey(nc => nc.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(skill => skill.SkillsList)
                .WithOne(hn => hn.Employee)
                .HasForeignKey(hn => hn.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(h => h.EventList)
                .WithOne(hn => hn.Employee)
                .HasForeignKey(hn => hn.EmployeeId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Employee>()
                .Property(nc => nc.Id)
                .HasDefaultValue(Guid.NewGuid());

            #endregion
            #region Accomplishment
            modelBuilder.Entity<Accomplishment>()
                .Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Accomplishment>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Accomplishment>()
                .Property(e => e.Id)
                .HasDefaultValue(Guid.NewGuid());
            #endregion
            #region Communication
            modelBuilder.Entity<Communication>()
                .Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Communication>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Communication>()
                .Property(e => e.Id)
                .HasDefaultValue(Guid.NewGuid());
            #endregion
            #region Event
            modelBuilder.Entity<Event>()
                .Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Event>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Event>()
                .Property(e => e.Id)
                .HasDefaultValue(Guid.NewGuid());
            #endregion
            #region Experience
            modelBuilder.Entity<Experience>()
                .Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Experience>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Experience>()
                .Property(e => e.Id)
                .HasDefaultValue(Guid.NewGuid());
            #endregion
            #region Skill
            modelBuilder.Entity<Skill>()
                .Property(e => e.CreatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Skill>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity<Skill>()
                .Property(e => e.Id)
                .HasDefaultValue(Guid.NewGuid());
            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
