using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		// Action methods
		public IActionResult Index()
		{
			IEnumerable<Product> productList = _unitOfWork.ProductRepo.GetAll(includeProps: "Category");
			return View(productList);      // .Net find View() in file Views/Home/Index.cshtml 
		}

		public IActionResult Details(int productId)
		{
			Product product = _unitOfWork.ProductRepo.Get(p => p.Id==productId, includeProps: "Category");
			return View(product);      // .Net find View() in file Views/Home/Index.cshtml 
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}