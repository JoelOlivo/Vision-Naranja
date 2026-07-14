using Microsoft.AspNetCore.Mvc;
using VisionNaranja.Services;
using VisionNaranja.ViewModels.Product;

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
            IEnumerable<GetProductViewModel> products = await _service.GetAllByEntrepreneurshipAsync(1);

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<GetProductViewModel> products = await _service.GetAllByEntrepreneurshipAsync(1);

            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            GetProductViewModel? product = await _service.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddProductViewModel viewModel)
        {
            bool result = await _service.AddAsync(viewModel, viewModel.Files);

            if (!result)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductViewModel viewModel)
        {
            UpdateProductViewModel model = new()
            {
                Id = id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price
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