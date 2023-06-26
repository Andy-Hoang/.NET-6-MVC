using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		//inject dependency in the constructor: unitOfWork already registered in Container
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			IEnumerable<Product> ProductList = _unitOfWork.ProductRepo.GetAll();
			return View(ProductList);
		}

		//Get
		public IActionResult Create()
		{
			return View();
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Product obj)
		{
			if (ModelState.IsValid)     //server side validation
			{
				_unitOfWork.ProductRepo.Add(obj);
				_unitOfWork.Save();      //actual action made to Database
				TempData["Success"] = "Product created successfully";
				return RedirectToAction("Index");       // if no Controller name, then default is the current Controller
			}
			return View(obj);       //pass the current 'obj' to see the current input values causing error (if in case of error)

		}

		//Get
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var productFromDB = _unitOfWork.ProductRepo.Get(c => c.Id == id);
			if (productFromDB == null)
			{
				return NotFound();
			}

			return View(productFromDB);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Product obj)
		{
			if (obj.Title == obj.ListPrice.ToString())
			{
				//key 'title' will bind to show error inside 'Title' prop of Product
				ModelState.TryAddModelError("title", "ListPrice cannot exactly match the Title.");
			}
			if (ModelState.IsValid)
			{
				_unitOfWork.ProductRepo.Update(obj);
				_unitOfWork.Save();
				TempData["Success"] = "Product edited successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		//Get
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var productFromDB = _unitOfWork.ProductRepo.Get(c => c.Id == id);
			if (productFromDB == null)
			{
				return NotFound();
			}

			return View(productFromDB);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		// Can pass here both Product obj or id
		// If pass obj: will now to trigger Delete( obj ) for POST instead of Delete (int) for GET
		// If pass id: func will be the same with Delete(int? id) for Get -> so have to change named to DeletePost()
		public IActionResult DeletePost(int? id)        //can pass here both Product obj or id | Pass id to mak
		{
			var productFromDB = _unitOfWork.ProductRepo.Get(c => c.Id == id);
			if (productFromDB == null)
			{
				return NotFound();
			}

			_unitOfWork.ProductRepo.Remove(productFromDB);     //Entity Framework does all the work: find the 'obj' by id in DB, look for what props changed, and update
			_unitOfWork.Save();
			TempData["Success"] = "Product deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
