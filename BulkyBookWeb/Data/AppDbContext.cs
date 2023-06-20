using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class AppDbContext: DbContext
    {
        //pass "options" -which are passed into subclass AppDbContext- into the base class DbContext by using ":base(options)"
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DBSet is like a table in SQL
        public DbSet<Category> Categories { get; set; }
    }
}
