using Microsoft.EntityFrameworkCore;

namespace Task_ModulesImplementation.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Reminder> Reminder { get; set; }
        public ApplicationContext() :base() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=.;Initial Catalog=Ringo_Media_Departments;Integrated Security=True;Encrypt=False");
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(d => d.parentDepartment)
                .WithMany(d => d.subDepartments)
                .HasForeignKey(d => d.parentDepartmentId);
        }

    }
}
