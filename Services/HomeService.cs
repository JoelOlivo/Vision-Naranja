using VisionNaranja.Data.Repositories;
using VisionNaranja.ViewModels.Home;

namespace VisionNaranja.Services
{
    public class HomeService
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductMediaRepository _productMediaRepository;

        public HomeService(
            ProductRepository productRepository,
            ProductMediaRepository productMediaRepository)
        {
            _productRepository = productRepository;
            _productMediaRepository = productMediaRepository;
        }

        public async Task<GetHomeViewModel> GetAsync()
        {
            var products = await _productRepository.GetAllDetailsAsync();

            foreach (var product in products)
                product.Media = await _productMediaRepository.GetByProductIdAsync(product.Id);

            return new GetHomeViewModel
            {
                Products = products
            };
        }
    }
}
