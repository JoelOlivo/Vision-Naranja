using VisionNaranja.Data.Repositories;
using VisionNaranja.Services.FileStorage;
using VisionNaranja.Services.Storage;
using VisionNaranja.ViewModels.Product;
using VisionNaranja.ViewModels.ProductMedia;

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

        public async Task<IEnumerable<GetProductViewModel>> GetAllByEntrepreneurshipAsync(int entrepreneurshipId)
        {
            return await _repository.GetAllByEntrepreneurshipAsync(entrepreneurshipId);
        }

        public async Task<GetProductViewModel?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(AddProductViewModel product, IEnumerable<IFormFile> files)
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
                await _productMediaRepository.AddAsync(new AddProductMediaViewModel
                (
                    file.FileName,
                    file.RelativePath,
                    isPrimary,
                    productId
                ));

                isPrimary = false;
            }

            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductViewModel model)
        { 
            return await _repository.UpdateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool mediasDeleted = await _productMediaRepository.DeleteByProductIdAsync(id);

            if (!mediasDeleted)
                return false;

            bool productDeleted = await _repository.DeleteAsync(id);

            if (!productDeleted)
                return false;

            _fileStorageService.DeleteDirectory(FileStorageFolder.Products, id);

            return true;
        }
    }
}