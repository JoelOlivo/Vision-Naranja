using Microsoft.AspNetCore.Mvc;
using VisionNaranja.Models;
using VisionNaranja.Services;
using VisionNaranja.ViewModels;

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
            IEnumerable<ProductViewModel> products = await _service.GetAllAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ProductViewModel> products = await _service.GetAllAsync();

            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            ProductViewModel? product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductViewModel viewModel)
        {
            ProductModel model = new()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ProductTypeId = viewModel.ProductTypeId,
                EntrepreneurshipId = viewModel.EntrepreneurshipId
            };

            bool result = await _service.AddAsync(model);

            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ProductViewModel viewModel)
        {
            ProductModel model = new()
            {
                Id = id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ProductTypeId = viewModel.ProductTypeId,
                EntrepreneurshipId = viewModel.EntrepreneurshipId
            };

            bool result = await _service.UpdateAsync(model);

            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _service.DeleteAsync(id);

            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }
    }
}
