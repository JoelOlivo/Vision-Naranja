using Microsoft.AspNetCore.Mvc;
using VisionNaranja.Data.Repositories;
using VisionNaranja.Services;

namespace VisionNaranja.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();

            return View(products);
        }
    }
}
