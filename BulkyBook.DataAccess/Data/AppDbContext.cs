﻿using BulkyBook.Models;
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
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Scifi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product 
                { 
                    Id = 1, 
                    Title = "Fortune of Time", 
                    Author = "Billy Spark",
                    Description = "shjfbv grtuhfeijksl fjbcndm fdhjkfn hfjdvc hdasmvn.",
                    ISBN = "SWE45678",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                },
                new Product
                {
                    Id = 2,
                    Title = "Alalala Alalala",
                    Author = "Billy Billy Billy Billy",
                    Description = "shjfbv grtuhfeijksl fjbcndm fdhjkfn hfjdvc hdasmvn.",
                    ISBN = "SWE45689",
                    ListPrice = 66,
                    Price = 60,
                    Price50 = 55,
                    Price100 = 50,
                    CategoryId = 2,
                },
                new Product
                {
                    Id = 3,
                    Title = "Hula Hula hula hula",
                    Author = "Andy Andy Andy",
                    Description = "shjfbv grtuhfeijksl fjbcndm fdhjkfn hfjdvc hdasmvn.",
                    ISBN = "SWE456567",
                    ListPrice = 88,
                    Price = 80,
                    Price50 = 75,
                    Price100 = 70,
                    CategoryId = 3,
                }
                );
        }
    }
}
