using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _db;
		public ICategoryRepository CategoryRepo { get; private set; }
		public IProductRepository ProductRepo { get; private set; }
		public UnitOfWork(AppDbContext db)
		{
			_db = db;
			CategoryRepo = new CategoryRepository(_db);
			ProductRepo = new ProductRepository(_db);
		}

		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
