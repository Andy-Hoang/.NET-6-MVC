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
	// Repository<Category>: base class, inherit the implementation of ICategoryRepository<Category> (to implement all its functions
	// ICategoryRepository: to inherit things specific to Category
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly AppDbContext _db;
		public CategoryRepository(AppDbContext db) : base(db) 	//pass the 'db' to the base class
		{
			_db = db;
		}
		

		public void Update(Category obj)
		{
			_db.Categories.Update(obj);     // why _db.Categories work: because 'Repository<Category>' so 'Categories' dbSet is already declared
		}
	}
}
