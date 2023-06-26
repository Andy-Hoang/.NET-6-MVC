using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.DataAccess.Data;

namespace BulkyBook.DataAccess.Repository
{
	// Repository<Product>: base class, inherit the implementation of ICategoryRepository<Category> (to implement all its functions
	// IProductRepository: to inherit things specific to Product
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly AppDbContext _db;
		public ProductRepository(AppDbContext db) : base(db) 	//pass the 'db' to the base class
		{
			_db = db;
		}
		

		public void Update(Product obj)
		{
			_db.Products.Update(obj);     // why _db.Products work: because 'Repository<Product>' so 'Categories' dbSet is already declared
		}
	}
}
