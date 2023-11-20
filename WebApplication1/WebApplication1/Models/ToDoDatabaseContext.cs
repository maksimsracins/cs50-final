using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ToDoDatabaseContext : DbContext
    {
        public ToDoDatabaseContext(DbContextOptions<ToDoDatabaseContext> options) : base(options)
        { }

        public DbSet<ToDo> ToDos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;

        //seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "work", CategoryName = "Work" },
                new Category { CategoryId = "home", CategoryName = "Home" },
                new Category { CategoryId = "other", CategoryName = "Other" }
                );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", StatusName = "Open" },
                new Status { StatusId = "closed", StatusName = "Completed" });
        }
    }
}
