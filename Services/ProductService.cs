using VisionNaranja.Data.Repositories;
using VisionNaranja.Models;
using VisionNaranja.Services.FileStorage;
using VisionNaranja.Services.Storage;
using VisionNaranja.ViewModels;

namespace VisionNaranja.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly ProductMediaRepository _productMediaRepository;
        private readonly FileStorageService _fileStorageService;

        public ProductService(
            ProductRepository repository, 
            ProductMediaRepository productMediaRepository, 
            FileStorageService fileStorageService)
        {
            _repository = repository;
            _productMediaRepository = productMediaRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(ProductModel product, IEnumerable<IFormFile> files)
        {
            int productId = await _repository.AddAsync(product);

            if (productId <= 0)
                return false;

            List<FileStorageModel> storedFiles = await _fileStorageService.SaveAsync(
                files,
                FileStorageFolder.Products,
                productId
            );

            bool isPrimary = true;

            foreach (var file in storedFiles)
            {
                await _productMediaRepository.AddAsync(new ProductMediaModel
                {
                    FileName = file.FileName,
                    MediaPath = file.RelativePath,
                    ProductId = productId,
                    IsPrimary = isPrimary
                });

                isPrimary = false;
            }

            return true;
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