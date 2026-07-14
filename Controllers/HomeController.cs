using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisionNaranja.Models;
using VisionNaranja.Services;
using VisionNaranja.ViewModels;

namespace VisionNaranja.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _service;

        public HomeController(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //IEnumerable<ProductViewModel> products = await _service.GetAllAsync();
            IEnumerable<ProductViewModel> products = [];
            return View(products);
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
