using CRUDOperationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDOperationProject.Entity
{
    public class EntityDbContext :DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)

        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1,
                    Name = "Akash",
                    Email = "abiakash1993@gmail.com",
                    DateOfBirth = "1993-09-02",
                    Gender = Gender.Male,
                    Description = "Junior Developer",
                    Department = "IT" });
        }
    }
}
