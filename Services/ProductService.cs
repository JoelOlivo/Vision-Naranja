using VisionNaranja.Data.Repositories;
using VisionNaranja.Models;
using VisionNaranja.ViewModels;

namespace VisionNaranja.Services
{
    public class ProductService(ProductRepository repository)
    {
        private readonly ProductRepository _repository = repository;

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(ProductModel model)
        {
            return await _repository.AddAsync(model);
        }

        public async Task<bool> UpdateAsync(ProductModel model)
        { 
            return await _repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}