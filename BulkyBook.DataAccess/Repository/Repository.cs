using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDbContext _db;
		// because we dont know what type (Category/ Product) yet, so cannot use db.Categories.Add()
		// so create an internal DbSet, so will equivalent like db.Categories = dbSet
		internal DbSet<T> dbSet;		

		public Repository(AppDbContext db)
		{
			_db = db;
			dbSet = _db.Set<T>();
			_db.Products.Include(p => p.Category).Include(p => p.CategoryId);		//sepecify related entity to be included in query results
		}

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProps = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.FirstOrDefault();
		}

		// includeProps sample: Category,CoverType
		public IEnumerable<T> GetAll(string? includeProps = null)
		{
			IQueryable<T> query = dbSet;
			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return query.ToList();
		}

		public void Remove(T entity)
		{
			dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
