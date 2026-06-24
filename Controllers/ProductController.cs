using Microsoft.AspNetCore.Mvc;
using VisionNaranja.Data.Repositories;

namespace VisionNaranja.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllAsync();

            return View(products);
        }
    }
}
