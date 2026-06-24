using VisionNaranja.Data.Repositories;
using VisionNaranja.ViewModels;

namespace VisionNaranja.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        //public Task<bool> AddAsync()
        //{ 
        
        //}
    }
}
