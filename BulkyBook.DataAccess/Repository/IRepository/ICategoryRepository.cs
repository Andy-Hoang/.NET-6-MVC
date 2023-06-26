using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		// define the extension where IRepository does not have
		void Update(Category obj);		// Why Update(): different types has different Update() implementation
	}
}
