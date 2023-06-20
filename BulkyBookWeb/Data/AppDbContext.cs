using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data
{
    public class AppDbContext: DbContext
    {
        //add "options" -which are passed into subclass AppDbContext- into the base class DbContext by using ":base(options)"
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
