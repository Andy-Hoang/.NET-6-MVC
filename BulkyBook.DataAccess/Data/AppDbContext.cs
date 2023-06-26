using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data
{
    public class AppDbContext: DbContext
    {
        // base(options): pass "options" -which are passed into subclass AppDbContext- into the base class DbContext by using ":base(options)"
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DBSet is like a table in SQL
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
		}
    }
}
