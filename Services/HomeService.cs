using VisionNaranja.Data.Repositories;
using VisionNaranja.ViewModels.Home;

namespace VisionNaranja.Services
{
    public class HomeService
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductMediaRepository _productMediaRepository;
        private readonly EntrepreneurRepository _entrepreneurRepository;
        private readonly EntrepreneurshipRepository _entrepreneurshipRepository;

        public HomeService(
            ProductRepository productRepository,
            ProductMediaRepository productMediaRepository,
            EntrepreneurRepository entrepreneurRepository,
            EntrepreneurshipRepository entrepreneurshipRepository)
        {
            _productRepository = productRepository;
            _productMediaRepository = productMediaRepository;
            _entrepreneurRepository = entrepreneurRepository;
            _entrepreneurshipRepository = entrepreneurshipRepository;
        }

        public async Task<HomeIndexViewModel> GetHomeAsync()
        {
            var products = await _productRepository.GetAllDetailsAsync();

            foreach (var product in products)
                product.Media = await _productMediaRepository.GetByProductIdAsync(product.Id);

            return new HomeIndexViewModel
            {
                Products = products
            };
        }

        public async Task<HomeEntrepreneurshipViewModel?> GetHomeEntrepreneurshipAsync(int entrepreneurshipId)
        {
            var entrepreneurship = await _entrepreneurshipRepository.GetByIdAsync(entrepreneurshipId);

            if (entrepreneurship == null)
                return null;

            return new HomeEntrepreneurshipViewModel
            {
                Entrepreneurship = entrepreneurship
            };
        }

        public async Task<HomeEntrepreneurViewModel?> GetHomeEntrepreneurAsync(int entrepreneurId)
        {
            var entrepreneur = await _entrepreneurRepository.GetByIdAsync(entrepreneurId);

            if (entrepreneur == null)
                return null;

            return new HomeEntrepreneurViewModel
            {
                Entrepreneur = entrepreneur
            };
        }
    }
}