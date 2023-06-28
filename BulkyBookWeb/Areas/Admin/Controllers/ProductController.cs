using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		//inject dependency in the constructor: unitOfWork already registered in Container
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
		{
			IEnumerable<Product> ProductList = _unitOfWork.ProductRepo.GetAll(includeProps: "Category");
			return View(ProductList);
		}
		
		//Get
		public IActionResult Upsert(int? id)
		{
			ProductVM productVM = new()
			{
				CategoryList = _unitOfWork.CategoryRepo.GetAll().Select(c => new SelectListItem             // EF Projection: convert Category type into SelectListItem type
				{
					Text = c.Name,
					Value = c.Id.ToString()
				}),
				Product = new Product()
			};
			if (id == null || id == 0)
            {
				// create
				return View(productVM);
			}
            else
            {
				// update
				productVM.Product = _unitOfWork.ProductRepo.Get(p => p.Id == id);
				return View(productVM);
			}
			
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)     //server side validation
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;		//get the path of wwwroot folder
				if(file != null)
                {
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);     //create a new unique filename but keep extension
					string productPath = Path.Combine(wwwRootPath, @"images\product");

					//check if there is already an image, then delete this old image
					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

						if(System.IO.File.Exists(oldImagePath))
                        {
							System.IO.File.Delete(oldImagePath);
                        }
                    }

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
						file.CopyTo(fileStream);
                    }

					productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

				if (productVM.Product.Id == 0)
                {
					_unitOfWork.ProductRepo.Add(productVM.Product);
				}
                else
                {
					_unitOfWork.ProductRepo.Update(productVM.Product);
				}

				_unitOfWork.Save();      //actual action made to Database
				TempData["Success"] = "Product created successfully";
				return RedirectToAction("Index");       // if no Controller name, then default is the current Controller
			}
            else 
			{
				// the productVM passed from the the Create Form will have CategoryList = null, so need to generate value again 
				productVM.CategoryList = _unitOfWork.CategoryRepo.GetAll().Select(c => new SelectListItem             // EF Projection: convert Category type into SelectListItem type
				{
					Text = c.Name,
					Value = c.Id.ToString()
				});
				return View(productVM);       //pass the current 'obj' to see the current input values causing error (if in case of error)
			}
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
